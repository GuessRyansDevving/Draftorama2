using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Draftorama.Models
{
    public class Card
    {
        #region Fields

        private string _cardName;
        private string _cardRarity;
        private Rating _cardRating;
        private List<string> _cardTypes;
        private string _imageFile;
        private ManaRelations _manaRelation;
        private string _setName;
        private List<string> _tags;

        #endregion Fields

        #region Constructors

        public Card(string cardStringIn, string setNameIn)
        {
            string[] cardLineParts = cardStringIn.Split(';');

            Name = cardLineParts[0];
            cardLineParts[0] = cardLineParts[0].Replace("_", " ");
            _cardName = cardLineParts[0];
            _cardTypes = ReadTypes(cardLineParts[1]);
            _cardRarity = ReadRarity(cardLineParts[2]);
            _imageFile = cardLineParts[3];
            _cardRating = ReadRatings(cardLineParts[4]);
            _manaRelation = new ManaRelations(cardLineParts[5]);
            cardLineParts[6] = cardLineParts[6].Replace("\r", "");
            _tags = ReadTags(cardLineParts[6]);
            _setName = setNameIn;
        }

        public Card(string nameIn, List<string> typesIn, string rarityIn, string imageIn,
            Rating ratingIn, ManaRelations manaRelationIn, List<string> tagsIn, string setNameIn)
        {
            _cardName = nameIn;
            _cardTypes = typesIn;
            _cardRarity = rarityIn;
            _imageFile = imageIn;
            _cardRating = ratingIn;
            _manaRelation = manaRelationIn;
            _tags = tagsIn;
            _setName = setNameIn;
        }

        private Card()
        {
        }

        #endregion Constructors

        //public enum Rarity { Common, Uncommon, Rare, Mythic, Special }

        //public enum Types { Artifact, Creature, Enchantment, Instant, Land, Plainswalker, Sorcery, Tribal }

        #region Properties

        public string CardName
        {
            get { return _cardName; }
            set { _cardName = value; }
        }

        public string CardRarity
        {
            get { return _cardRarity; }
            set { _cardRarity = value; }
        }

        public Rating CardRating
        {
            get { return _cardRating; }
            set { _cardRating = value; }
        }

        public List<string> CardTypes
        {
            get { return _cardTypes; }
            set { _cardTypes = value; }
        }

        public string ImageFile
        {
            get { return _imageFile; }
            set { _imageFile = value; }
        }

        public ManaRelations ManaRelation
        {
            get { return _manaRelation; }
            set { _manaRelation = value; }
        }

        public string Name { get; set; }

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public List<string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        #endregion Properties

        #region Methods

        public void ExportToJSON()
        {
            File.WriteAllText($"wwwroot/sets/{SetName}/{CardName}.json", JsonConvert.SerializeObject(this));
        }

        private string ReadRarity(string rarityStringIn)
        {
            string r;

            switch (rarityStringIn)
            {
                case "C":
                    r = "Common";
                    break;

                case "U":
                    r = "Uncommon";
                    break;

                case "R":
                    r = "Rare";
                    break;

                case "M":
                    r = "Mythic";
                    break;

                default:
                    r = "Special";
                    break;
            }

            return r;
        }

        private Card.Rating ReadRatings(string ratingStringIn)
        {
            double?[] ratingsIn = { null, null, null, null };
            int ratingCount = 0;

            foreach (string ratingString in ratingStringIn.Split(','))
            {
                ratingsIn[ratingCount] = Convert.ToDouble(ratingString);
            }
            return new Card.Rating((double)ratingsIn[0], ratingsIn[1], ratingsIn[2], ratingsIn[3]);
        }

        private List<string> ReadTags(string tagStringIn)
        {
            List<string> tags = new List<string>();

            foreach (string tag in tagStringIn.Split(','))
            {
                tags.Add(tag);
            }

            return tags;
        }

        private List<string> ReadTypes(string typesStringIn)
        {
            List<string> cardTypesFound = new List<string>();

            foreach (string cardTypeIn in typesStringIn.Split(','))
            {
                if (cardTypeIn == "Artifact" || cardTypeIn == "Creature" || cardTypeIn == "Enchantment" || cardTypeIn == "Instant" ||
                    cardTypeIn == "Land" || cardTypeIn == "Plainswalker" || cardTypeIn == "Sorcery" || cardTypeIn == "Tribal")
                {
                    cardTypesFound.Add(cardTypeIn);
                }
            }

            return cardTypesFound;
        }

        #endregion Methods

        #region Structs

        public struct ManaRelations
        {
            #region Fields

            private List<Mana> _abilityCosts;
            private List<Mana> _castingCosts;
            private int[] _colorIdentity;
            private int _convertedManaCost;
            private List<Mana> _generatedMana;

            #endregion Fields

            #region Constructors

            public ManaRelations(string manaRelationsIn)
            {
                _abilityCosts = new List<Mana>();
                _castingCosts = new List<Mana>();
                _generatedMana = new List<Mana>();
                _colorIdentity = new int[] { 0, 0, 0, 0, 0, 0 };
                _convertedManaCost = 0;

                string[] manaRelationsInParts = manaRelationsIn.Split(':');

                _castingCosts = ReadCostsFromString(manaRelationsInParts[0], "CastingCost");
                if (manaRelationsInParts.Length > 1)
                {
                    _abilityCosts = ReadCostsFromString(manaRelationsInParts[1], "AbilityCost");
                }
                if (manaRelationsInParts.Length > 2)
                {
                    _generatedMana = ReadCostsFromString(manaRelationsInParts[2], "ManaGenerated");
                }
                _convertedManaCost = CalculateCMC();
                _colorIdentity = CalculateColorIdentity();
            }

            #endregion Constructors

            #region Properties

            public List<Mana> AbilityCosts
            {
                get { return _abilityCosts; }
                set { _abilityCosts = value; }
            }

            public List<Mana> CastingCosts
            {
                get { return _castingCosts; }
                set { _castingCosts = value; }
            }

            public int[] ColorIdentity
            {
                get { return _colorIdentity; }
                set { _colorIdentity = value; }
            }

            public int ConvertedManaCost
            {
                get { return _convertedManaCost; }
                set { _convertedManaCost = value; }
            }

            public List<Mana> GeneratedMana
            {
                get { return _generatedMana; }
                set { _generatedMana = value; }
            }

            #endregion Properties

            #region Methods

            private int CalculateCMC()
            {
                int cost = 0;
                if (CastingCosts.Count > 0)
                {
                    cost = CastingCosts.First().Generic + CastingCosts.First().Colorless + CastingCosts.First().White + CastingCosts.First().Blue + CastingCosts.First().Black + CastingCosts.First().Red + CastingCosts.First().Green;
                }

                return cost;
            }

            private int[] CalculateColorIdentity()
            {
                int[] identity = new int[] { 0, 0, 0, 0, 0, 0 };

                foreach (Mana m in CastingCosts)
                {
                    identity[0] += m.White * m.IdentityWeight;
                    identity[1] += m.Blue * m.IdentityWeight;
                    identity[2] += m.Black * m.IdentityWeight;
                    identity[3] += m.Red * m.IdentityWeight;
                    identity[4] += m.Green * m.IdentityWeight;
                    identity[5] += m.Colorless * m.IdentityWeight;
                }
                foreach (Mana m in AbilityCosts)
                {
                    identity[0] += m.White * m.IdentityWeight;
                    identity[1] += m.Blue * m.IdentityWeight;
                    identity[2] += m.Black * m.IdentityWeight;
                    identity[3] += m.Red * m.IdentityWeight;
                    identity[4] += m.Green * m.IdentityWeight;
                    identity[5] += m.Colorless * m.IdentityWeight;
                }
                foreach (Mana m in GeneratedMana)
                {
                    identity[0] += m.White * m.IdentityWeight;
                    identity[1] += m.Blue * m.IdentityWeight;
                    identity[2] += m.Black * m.IdentityWeight;
                    identity[3] += m.Red * m.IdentityWeight;
                    identity[4] += m.Green * m.IdentityWeight;
                    identity[5] += m.Colorless * m.IdentityWeight;
                }

                return identity;
            }

            private List<Mana> ReadCostsFromString(string manaStringIn, string relationType)
            {
                List<Mana> ManaAssociations = new List<Mana>();

                foreach (string manaString in manaStringIn.Split(','))
                {
                    Mana newMana = new Mana(manaString, relationType);

                    ManaAssociations.Add(newMana);
                }

                return ManaAssociations;
            }

            #endregion Methods
        }

        public struct Rating
        {
            #region Fields

            private double? _averageUserRating;
            private double? _draftSimRating;
            private double? _lrCastRating;
            private double _ryanRating;
            private double _totalRating;

            #endregion Fields

            #region Constructors

            public Rating(double ryanRatingIn, double? averageUserRatingIn, double? draftSimRatingIn, double? lrCastRatingIn)
            {
                _averageUserRating = averageUserRatingIn;
                _draftSimRating = draftSimRatingIn;
                _lrCastRating = lrCastRatingIn;
                _ryanRating = ryanRatingIn;
                _totalRating = 0;

                CalculateTotalRating(_ryanRating, _averageUserRating, _draftSimRating, _lrCastRating);
            }

            #endregion Constructors

            #region Properties

            public double? AverageUserRating
            {
                get { return _averageUserRating; }
                set { _averageUserRating = value; }
            }

            public double? DraftSimRating
            {
                get { return _draftSimRating; }
                set { _draftSimRating = value; }
            }

            public double? LRCastRating
            {
                get { return _lrCastRating; }
                set { _lrCastRating = value; }
            }

            public double RyanRating
            {
                get { return _ryanRating; }
                set { _ryanRating = value; }
            }

            public double TotalRating
            {
                get { return _totalRating; }
                set { _totalRating = value; }
            }

            #endregion Properties

            #region Methods

            private void CalculateTotalRating(double ryanRating, double? averageUserRating, double? draftSimRating, double? lrCastRating)
            {
                double runningTotal = ryanRating;
                int runningRatingCount = 1;

                if (averageUserRating != null)
                {
                    runningTotal += (double)averageUserRating;
                    runningRatingCount++;
                }
                if (draftSimRating != null)
                {
                    runningTotal += (double)draftSimRating;
                    runningRatingCount++;
                }
                if (lrCastRating != null)
                {
                    runningTotal += (double)lrCastRating;
                    runningRatingCount++;
                }

                _totalRating = runningTotal / runningRatingCount;
            }

            #endregion Methods
        }

        #endregion Structs
    }
}
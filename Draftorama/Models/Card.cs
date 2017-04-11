using System.Collections.Generic;

namespace Draftorama.Models
{
    public class Card
    {
        #region Fields

        private string _cardName;
        private Rarity _cardRarity;
        private IEnumerable<Types> _cardTypes;
        private Mana _castingCost;
        private int _convertedManaCost;
        private string _imageFile;
        private IEnumerable<Mana> _manaAssociations;
        private IEnumerable<double> _ratings;
        private IEnumerable<string> _tags;
        private double _totalRating;

        #endregion Fields

        #region Constructors

        public Card(string nameIn, Rarity rarityIn, IEnumerable<Types> typesIn, string imageIn, IEnumerable<Mana> manaCostsIn, IEnumerable<string> tagsIn, IEnumerable<double> ratingsIn)
        {
            _cardName = nameIn;
            _cardRarity = rarityIn;
            _cardTypes = typesIn;
            _imageFile = imageIn;
            _manaAssociations = manaCostsIn;
            _tags = tagsIn;
            _ratings = ratingsIn;
        }

        public Card()
        {
        }

        #endregion Constructors

        #region Enums

        public enum Rarity
        {
            Common = 0,
            Uncommon = 1,
            Rare = 2,
            Mythic = 3,
            Special = 4
        }

        public enum Types
        {
            Artifact = 0,
            Creature = 1,
            Enchantment = 2,
            Instant = 3,
            Land = 4,
            Plainswalker = 5,
            Sorcery = 6,
            Tribal = 7
        }

        #endregion Enums

        #region Properties

        public string CardName
        {
            get { return _cardName; }
            set { _cardName = value; }
        }

        public Rarity CardRarity
        {
            get { return _cardRarity; }
            set { _cardRarity = value; }
        }

        public IEnumerable<Types> CardTypes
        {
            get { return _cardTypes; }
            set { _cardTypes = value; }
        }

        public Mana CastingCost
        {
            get { return _castingCost; }
            set { _castingCost = value; }
        }

        public int ConvertedManaCost
        {
            get { return _convertedManaCost; }
            set { _convertedManaCost = value; }
        }

        public string ImageFile
        {
            get { return _imageFile; }
            set { _imageFile = value; }
        }

        public IEnumerable<Mana> ManaAssociations
        {
            get { return _manaAssociations; }
            set { _manaAssociations = value; }
        }

        public IEnumerable<double> Ratings
        {
            get { return _ratings; }
            set { _ratings = value; }
        }

        public IEnumerable<string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        public double TotalRating
        {
            get { return _totalRating; }
            set { _totalRating = value; }
        }

        #endregion Properties

        #region Structs

        public struct Mana
        {
            #region Fields

            private int _black;

            private int _blue;

            private double[] _colorIdentity;

            private int _colorless;

            private int _generic;

            private int _green;

            private ManaRelation _manaRelation;

            private int _red;

            private string _stringFormat;

            private double _weight;

            private int _white;

            #endregion Fields

            #region Enums

            public enum ManaRelation
            {
                CastingCost = 0,
                AlternateCastingCost = 1,
                ActivatedAbility = 2,
                Fee = 3,
                GeneratedMana = 4
            }

            public enum ManaTypes
            {
                Black = 0,
                Blue = 1,
                Colorless = 2,
                Generic = 3,
                Green = 4,
                Hybrid = 5,
                Red = 6,
                White = 7
            }

            #endregion Enums

            #region Properties

            public int Black
            {
                get { return _black; }
                set { _black = value; }
            }

            public int Blue
            {
                get { return _blue; }
                set { _blue = value; }
            }

            public double[] ColorIdentity
            {
                get { return _colorIdentity; }
                set { _colorIdentity = value; }
            }

            public int Colorless
            {
                get { return _colorless; }
                set { _colorless = value; }
            }

            public ManaRelation CostType
            {
                get { return _manaRelation; }
                set { _manaRelation = value; }
            }

            public int Generic
            {
                get { return _generic; }
                set { _generic = value; }
            }

            public int Green
            {
                get { return _green; }
                set { _green = value; }
            }

            public int Red
            {
                get { return _red; }
                set { _red = value; }
            }

            public string StringFormat
            {
                get { return _stringFormat; }
                set { _stringFormat = value; }
            }

            public double Weight
            {
                get { return _weight; }
                set { _weight = value; }
            }

            public int White
            {
                get { return _white; }
                set { _white = value; }
            }

            #endregion Properties

            #region Methods

            public int calculateCMC()
            {
                int cost = this.Generic + this.Colorless + this.White + this.Blue + this.Black + this.Red + this.Green;
                return cost;
            }

            #endregion Methods
        }

        #endregion Structs
    }
}
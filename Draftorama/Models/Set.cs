using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Draftorama.Models
{
    public class Set : IPool
    {
        #region Fields

        private IEnumerable<Card> _cardsInSet;

        #endregion Fields

        #region Properties

        public IEnumerable<Card> CardsInSet
        {
            get { return _cardsInSet; }
            set { _cardsInSet = value; }
        }

        #endregion Properties

        //public void ExportToJSON(string setName)
        //{
        //    JsonSerializer js = new JsonSerializer();
        //    using (var fileStream = new FileStream(String.Format(setName + ".json"), FileMode.OpenOrCreate))
        //    using (var streamWriter = new StreamWriter(fileStream))
        //    using (var jw = new JsonTextWriter(streamWriter))
        //    {
        //        string output = JsonConvert.SerializeObject(_cardsInSet, Formatting.Indented, new JsonSerializerSettings
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        });

        //        jw.WriteRawValue(output);
        //    }
        //}

        #region Methods

        public IEnumerable<Card> GetAllCards()
        {
            throw new NotImplementedException();
        }

        //public void ImportFromJSON(string jsonFile)
        //{
        //    JsonSerializer js = new JsonSerializer();
        //    using (var fileStream = new FileStream(jsonFile, FileMode.Open))
        //    using (var streamReader = new StreamReader(fileStream))
        //    using (var jw = new JsonTextReader(streamReader))
        //    {
        //        jw.Read();

        //        string output = JsonConvert.SerializeObject(_cardsInSet, Formatting.Indented, new JsonSerializerSettings
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        });

        //        //jw.WriteRawValue(output);
        //    }
        //}

        //public void ImportSetFromJS(string setName)
        //{
        //    string setText = File.ReadAllText(setName);
        //    foreach (string cardText in setText.Split('{'))
        //    {
        //        if (cardText.Contains("name"))
        //        {
        //            Card newCard = new Card();

        //            foreach (string cardTextPart in cardText.Split(','))
        //            {
        //                if (cardTextPart.Contains("name:"))
        //                {
        //                    newCard.CardName = cardTextPart.Replace(" name: ", "").Replace("\"", "");
        //                }
        //                else if (cardTextPart.Contains("castingcost1:"))
        //                {
        //                    string manaText = cardTextPart.Replace(" castingcost1: ", "").Replace("\"", "");
        //                    Card.Mana newMana = new Card.Mana();

        //                    newMana.White = manaText.Count(x => x == 'W');
        //                    manaText.Replace("W", "");
        //                    newMana.Blue = manaText.Count(x => x == 'U');
        //                    manaText.Replace("U", "");
        //                    newMana.Black = manaText.Count(x => x == 'B');
        //                    manaText.Replace("B", "");
        //                    newMana.Red = manaText.Count(x => x == 'R');
        //                    manaText.Replace("R", "");
        //                    newMana.Green = manaText.Count(x => x == 'G');
        //                    manaText.Replace("G", "");
        //                }
        //                else if (cardTextPart.Contains("castingcost2:"))
        //                {
        //                    //newCard.ManaCosts = newCard.ManaCosts.Append<>
        //                }
        //                else if (cardTextPart.Contains("type:"))
        //                {
        //                    string typeText = cardTextPart.Replace(" type: ", "").Replace("\"", "");

        //                    if (typeText.Contains("Creature"))
        //                    {
        //                        newCard.CardTypes.Append(Card.Types.Creature);
        //                    }
        //                    else if (typeText.Contains("Spell"))
        //                    {
        //                        //newCard.CardTypes.Append(Card.Types.Sorcery);
        //                    }
        //                    else if (typeText.Contains("Instant"))
        //                    {
        //                        newCard.CardTypes.Append(Card.Types.Instant);
        //                    }
        //                    else if (typeText.Contains("Removal"))
        //                    {
        //                        newCard.CardTypes.Append(Card.Types.Instant);
        //                        newCard.Tags.Append("Removal");
        //                    }
        //                    else if (typeText.Contains("Land"))
        //                    {
        //                        newCard.CardTypes.Append(Card.Types.Land);
        //                    }
        //                }
        //                else if (cardTextPart.Contains("rarity:"))
        //                {
        //                    string rarityText = cardTextPart.Replace(" rarity: ", "").Replace("\"", "");
        //                    if (rarityText.Contains("C"))
        //                    {
        //                        newCard.CardRarity = Card.Rarity.Common;
        //                    }
        //                    else if (rarityText.Contains("U"))
        //                    {
        //                        newCard.CardRarity = Card.Rarity.Uncommon;
        //                    }
        //                    else if (rarityText.Contains("R"))
        //                    {
        //                        newCard.CardRarity = Card.Rarity.Rare;
        //                    }
        //                    else if (rarityText.Contains("M"))
        //                    {
        //                        newCard.CardRarity = Card.Rarity.Mythic;
        //                    }
        //                }
        //                else if (cardTextPart.Contains("myrating:"))
        //                {
        //                    string ratingText = cardTextPart.Replace(" myrating: ", "").Replace("\"", "");

        //                    newCard.TotalRating = Convert.ToDouble(ratingText);
        //                }
        //                else if (cardTextPart.Contains("image:"))
        //                {
        //                    string imageText = cardTextPart.Replace(" image: ", "").Replace("\"", "");
        //                }
        //                else if (cardTextPart.Contains("cmc:"))
        //                {
        //                    //
        //                }
        //                else if (cardTextPart.Contains("colors:"))
        //                {
        //                    //
        //                }
        //            }
        //        }
        //    }
        //}

        public void OpenSetFromCSV(string csvFileName)
        {
            CardsInSet = Enumerable.Empty<Card>();
            char[] splitChars = { '\r', '\n' };
            foreach (string cardLine in File.ReadAllText(csvFileName).Split(splitChars))
            {
                CardsInSet.Append(ReadCard(cardLine));
            }
        }

        public Card ReadCard(string cardTextIn)
        {
            string[] cardLineParts = cardTextIn.Split(';');

            //newCard.CardName = cardLineParts[0];

            //newCard.CardTypes = ReadTypes(cardLineParts[1]);

            //newCard.CardRarity = ReadRarity(cardLineParts[2]);

            //newCard.ImageFile = cardLineParts[3];

            //IEnumerable<double> ratings = ReadRatings(cardLineParts[4]);
            //newCard.TotalRating = ratings.First();
            //newCard.Ratings = ratings;

            //IEnumerable<Card.Mana> manaAssoc = ReadManaAssociations(cardLineParts[5]);
            //newCard.CastingCost = manaAssoc.First();
            //newCard.ManaAssociations = manaAssoc;

            //newCard.ConvertedManaCost = manaAssoc.First().calculateCMC();

            //newCard.Tags = ReadTags(cardLineParts[6]);

            return new Card(cardLineParts[0], ReadTypes(cardLineParts[1]), ReadRarity(cardLineParts[2]), cardLineParts[3],
                ReadRatings(cardLineParts[4]), ReadManaAssociations(cardLineParts[5]), ReadTags(cardLineParts[6]));
        }

        public IEnumerable<Card.Mana> ReadManaAssociations(string manaStringIn)
        {
            IEnumerable<Card.Mana> ManaAssociations = Enumerable.Empty<Card.Mana>();

            foreach (string manaString in manaStringIn.Split(','))
            {
                Card.Mana newMana = new Card.Mana();

                newMana.StringFormat = manaString;

                if (manaString.First() == 'X')
                {
                    newMana.Generic = 0;
                }
                else
                {
                    newMana.Generic = Convert.ToInt32(manaString.First());
                }

                newMana.White = manaString.Count(x => x == 'W');
                newMana.Blue = manaString.Count(x => x == 'U');
                newMana.Black = manaString.Count(x => x == 'B');
                newMana.Red = manaString.Count(x => x == 'R');
                newMana.Green = manaString.Count(x => x == 'G');
                newMana.Colorless = manaString.Count(x => x == 'C');

                ManaAssociations.Append(newMana);
            }

            return ManaAssociations;
        }

        public Card.Rarity ReadRarity(string rarityStringIn)
        {
            Card.Rarity r;

            switch (rarityStringIn)
            {
                case "C":
                    r = Card.Rarity.Common;
                    break;

                case "U":
                    r = Card.Rarity.Uncommon;
                    break;

                case "R":
                    r = Card.Rarity.Rare;
                    break;

                case "M":
                    r = Card.Rarity.Mythic;
                    break;

                default:
                    r = Card.Rarity.Special;
                    break;
            }

            return r;
        }

        public IEnumerable<double> ReadRatings(string ratingStringIn)
        {
            IEnumerable<double> ratings = Enumerable.Empty<double>();

            foreach (string ratingString in ratingStringIn.Split(','))
            {
                double rating = Convert.ToDouble(ratingString);
                ratings.Append(rating);
            }

            double averageRating = ratings.Average();
            ratings.Prepend(averageRating);

            return ratings;
        }

        public IEnumerable<string> ReadTags(string tagStringIn)
        {
            IEnumerable<string> tags = Enumerable.Empty<string>();

            foreach (string tag in tagStringIn.Split(','))
            {
                tags.Append(tag);
            }

            return tags;
        }

        public IEnumerable<Card.Types> ReadTypes(string typesStringIn)
        {
            IEnumerable<Card.Types> cardTypesFound = Enumerable.Empty<Card.Types>();

            foreach (string cardTypeIn in typesStringIn.Split(','))
            {
                switch (cardTypeIn)
                {
                    case "Artifact":
                        cardTypesFound.Append(Card.Types.Artifact);
                        break;

                    case "Creature":
                        cardTypesFound.Append(Card.Types.Creature);
                        break;

                    case "Enchantment":
                        cardTypesFound.Append(Card.Types.Enchantment);
                        break;

                    case "Instant":
                        cardTypesFound.Append(Card.Types.Instant);
                        break;

                    case "Land":
                        cardTypesFound.Append(Card.Types.Land);
                        break;

                    case "Plainswalker":
                        cardTypesFound.Append(Card.Types.Plainswalker);
                        break;

                    case "Sorcery":
                        cardTypesFound.Append(Card.Types.Sorcery);
                        break;

                    case "Tribal":
                        cardTypesFound.Append(Card.Types.Tribal);
                        break;

                    default:
                        // throw some exception I guess
                        break;
                }
            }

            return cardTypesFound;
        }

        #endregion Methods
    }
}
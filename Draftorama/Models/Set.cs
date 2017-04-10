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

        #region Methods

        public void ExportToJSON()
        {
            JsonSerializer js = new JsonSerializer();
            FileStream sw = File.OpenWrite("Blah.json");
            JsonTextWriter jw = new JsonTextWriter(sw);

            string output = JsonConvert.SerializeObject(_cardsInSet, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            jw.WriteRawValue(output);
            jw.Close();
            sw.Close();
        }

        public IEnumerable<Card> GetAllCards()
        {
            throw new NotImplementedException();
        }

        public void ImportSetFromJS(string setName)
        {
            string setText = File.ReadAllText(setName);
            foreach (string cardText in setText.Split('{'))
            {
                if (cardText.Contains("name"))
                {
                    Card newCard = new Card();

                    foreach (string cardTextPart in cardText.Split(','))
                    {
                        if (cardTextPart.Contains("name:"))
                        {
                            newCard.CardName = cardTextPart.Replace(" name: ", "").Replace("\"", "");
                        }
                        else if (cardTextPart.Contains("castingcost1:"))
                        {
                            string manaText = cardTextPart.Replace(" castingcost1: ", "").Replace("\"", "");
                            Card.Mana newMana = new Card.Mana();

                            newMana.White = manaText.Count(x => x == 'W');
                            manaText.Replace("W", "");
                            newMana.Blue = manaText.Count(x => x == 'U');
                            manaText.Replace("U", "");
                            newMana.Black = manaText.Count(x => x == 'B');
                            manaText.Replace("B", "");
                            newMana.Red = manaText.Count(x => x == 'R');
                            manaText.Replace("R", "");
                            newMana.Green = manaText.Count(x => x == 'G');
                            manaText.Replace("G", "");
                        }
                        else if (cardTextPart.Contains("castingcost2:"))
                        {
                            //newCard.ManaCosts = newCard.ManaCosts.Append<>
                        }
                        else if (cardTextPart.Contains("type:"))
                        {
                            string typeText = cardTextPart.Replace(" type: ", "").Replace("\"", "");

                            if (typeText.Contains("Creature"))
                            {
                                newCard.CardTypes.Append(Card.Types.Creature);
                            }
                            else if (typeText.Contains("Spell"))
                            {
                                //newCard.CardTypes.Append(Card.Types.Sorcery);
                            }
                            else if (typeText.Contains("Instant"))
                            {
                                newCard.CardTypes.Append(Card.Types.Instant);
                            }
                            else if (typeText.Contains("Removal"))
                            {
                                newCard.CardTypes.Append(Card.Types.Instant);
                                newCard.Tags.Append("Removal");
                            }
                            else if (typeText.Contains("Land"))
                            {
                                newCard.CardTypes.Append(Card.Types.Land);
                            }
                        }
                        else if (cardTextPart.Contains("rarity:"))
                        {
                            string rarityText = cardTextPart.Replace(" rarity: ", "").Replace("\"", "");
                            if (rarityText.Contains("C"))
                            {
                                newCard.CardRarity = Card.Rarity.Common;
                            }
                            else if (rarityText.Contains("U"))
                            {
                                newCard.CardRarity = Card.Rarity.Uncommon;
                            }
                            else if (rarityText.Contains("R"))
                            {
                                newCard.CardRarity = Card.Rarity.Rare;
                            }
                            else if (rarityText.Contains("M"))
                            {
                                newCard.CardRarity = Card.Rarity.Mythic;
                            }
                        }
                        else if (cardTextPart.Contains("myrating:"))
                        {
                            string ratingText = cardTextPart.Replace(" myrating: ", "").Replace("\"", "");

                            newCard.TotalRating = Convert.ToDouble(ratingText);
                        }
                        else if (cardTextPart.Contains("image:"))
                        {
                            string imageText = cardTextPart.Replace(" image: ", "").Replace("\"", "");
                        }
                        else if (cardTextPart.Contains("cmc:"))
                        {
                            //
                        }
                        else if (cardTextPart.Contains("colors:"))
                        {
                            //
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}
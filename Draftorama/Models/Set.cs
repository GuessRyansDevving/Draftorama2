using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Draftorama.Models
{
    public class Set
    {
        #region Private Fields

        private List<Card> _cardsInSet;
        private string _setName;

        #endregion Private Fields

        #region Public Constructors

        public Set(string setNameIn)
        {
            _setName = setNameIn;
            _cardsInSet = new List<Card>();
        }

        #endregion Public Constructors

        #region Public Properties

        public List<Card> CardsInSet
        {
            get { return _cardsInSet; }
            set { _cardsInSet = value; }
        }

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public void ExportToJSON()
        {
            File.WriteAllText($"wwwroot/sets/{_setName}/{_setName}.json", JsonConvert.SerializeObject(CardsInSet));
        }

        public void ImportFromJSON()
        {
            _cardsInSet = JsonConvert.DeserializeObject<List<Card>>(File.ReadAllText($"wwwroot/sets/{_setName}/{_setName}.json"));
        }

        public void ImportFromTXT()
        {
            CardsInSet = new List<Card>();
            char[] splitChars = { '\r', '\n' };
            foreach (string cardLine in File.ReadAllText($"wwwroot/sets/{_setName}/{_setName}.txt").Split('\n'))
            {
                CardsInSet.Add(new Card(cardLine, _setName));
            }
        }

        #endregion Public Methods
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Draftorama.Models
{
    public class Set
    {
        #region Private Fields

        private List<Card> _cardsInSet;
        private string _setName;
        private PackInfo _setPackInfo;

        #endregion Private Fields

        #region Public Constructors

        public Set(string setNameIn)
        {
            _setName = setNameIn;
            _cardsInSet = new List<Card>();
            SetPackInfo = new PackInfo(14, 1, 3, 10);
        }

        #endregion Public Constructors

        #region Public Properties

        public List<Card> CardsInSet
        {
            get { return _cardsInSet; }
            set { _cardsInSet = value; }
        }

        public List<Card> Commons
        {
            get { return _cardsInSet.Where(x => x.CardRarity == "Common").ToList(); }
        }

        public List<Card> Mythics
        {
            get { return _cardsInSet.Where(x => x.CardRarity == "Mythic").ToList(); }
        }

        public List<Card> Rares
        {
            get { return _cardsInSet.Where(x => x.CardRarity == "Rare").ToList(); }
        }

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public PackInfo SetPackInfo
        {
            get { return _setPackInfo; }
            set { _setPackInfo = value; }
        }

        public List<Card> Uncommons
        {
            get { return _cardsInSet.Where(x => x.CardRarity == "Uncommon").ToList(); }
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

        #region Public Structs

        public struct PackInfo
        {
            #region Public Constructors

            public PackInfo(int maxCardsInPackIn = 14, int maxRaresInPackIn = 1, int maxUncommonsInPackIn = 3, int maxCommonsInPackIn = 10)
            {
                MaxCardsInPack = maxCardsInPackIn;
                MaxRaresInPack = maxRaresInPackIn;
                MaxUncommonsInPack = maxUncommonsInPackIn;
                MaxCommonsInPack = maxCommonsInPackIn;
            }

            #endregion Public Constructors

            #region Public Properties

            public int MaxCardsInPack { get; private set; }

            public int MaxCommonsInPack { get; private set; }

            public int MaxRaresInPack { get; private set; }

            public int MaxUncommonsInPack { get; private set; }

            #endregion Public Properties
        }

        #endregion Public Structs
    }
}
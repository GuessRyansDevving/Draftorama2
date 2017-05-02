using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draftorama.Models
{
    public class Pack
    {
        #region Private Fields

        private List<Card> _cardsInPack;
        private int[] _packColorBalance;
        private Set _packSet;
        private Random _rand;
        private string _setName;

        #endregion Private Fields

        #region Public Constructors

        public Pack(Set draftSetIn)
        {
            PackSet = draftSetIn;

            CardsInPack = new List<Card>();

            GeneratePack();
        }

        public Pack()
        {
            CardsInPack = new List<Card>();
        }

        #endregion Public Constructors

        #region Public Properties

        public List<Card> CardsInPack
        {
            get { return _cardsInPack; }
            set { _cardsInPack = value; }
        }

        public int NumCardsInPack
        {
            get { return CardsInPack.Count; }
        }

        public int NumCommonsInPack
        {
            get { return CardsInPack.Where(x => x.CardRarity == "Common").Count(); }
        }

        public int NumRaresInPack
        {
            get { return CardsInPack.Where(x => (x.CardRarity == "Rare" || x.CardRarity == "Mythic")).Count(); }
        }

        public int NumUncommonsInPack
        {
            get { return CardsInPack.Where(x => x.CardRarity == "Uncommon").Count(); }
        }

        public Set PackSet
        {
            get { return _packSet; }
            set { _packSet = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public void ExportToJSON()
        {
            File.WriteAllText($"wwwroot/sets/{_setName}/GeneratedPack.json", JsonConvert.SerializeObject(CardsInPack));
        }

        public void GeneratePack()
        {
            _rand = new Random();
            _packColorBalance = new int[] { 0, 0, 0, 0, 0 };
            BuildPackCommons();
            BuildPackUncommons();
            BuildPackRare();
            //BuildPackSpecial();
            CardsInPack.Reverse();
            //ExportToJSON();ortToJSON();
        }

        #endregion Public Methods

        #region Private Methods

        private void BuildPackCommons()
        {
            int infiniteLoopKiller = 0;

            while (NumCommonsInPack < PackSet.SetPackInfo.MaxCommonsInPack)
            {
                Card c = PackSet.Commons.ElementAt(_rand.Next(PackSet.Commons.Count));

                if (!CardsInPack.Contains(c))
                {
                    int[] tempArray = new int[5];
                    _packColorBalance.CopyTo(tempArray, 0);
                    int count = 0;

                    foreach (int colorVal in tempArray)
                    {
                        if (c.ManaRelation.ColorIdentity[count] > 0)
                        {
                            tempArray[count]++;
                        }
                        count++;
                    }
                    if (tempArray.Max() <= tempArray.Min() + 2)
                    {
                        tempArray.CopyTo(_packColorBalance, 0);
                        CardsInPack.Add(c);
                    }
                }
                infiniteLoopKiller++;
                if (infiniteLoopKiller > 100)
                {
                    CardsInPack.RemoveAll(x => x.CardRarity == "Common");
                    infiniteLoopKiller = 0;
                }
            }
        }

        private void BuildPackRare()
        {
            if (_rand.Next(8) == 7)
            {
                CardsInPack.Add(PackSet.Mythics.ElementAt(_rand.Next(PackSet.Mythics.Count)));
            }
            else
            {
                CardsInPack.Add(PackSet.Rares.ElementAt(_rand.Next(PackSet.Rares.Count)));
            }
        }

        private void BuildPackSpecial()
        {
            throw new NotImplementedException();
        }

        private void BuildPackUncommons()
        {
            int infiniteLoopKiller = 0;

            while (NumUncommonsInPack < PackSet.SetPackInfo.MaxUncommonsInPack)
            {
                Card c = PackSet.Uncommons.ElementAt(_rand.Next(PackSet.Uncommons.Count));

                if (!CardsInPack.Contains(c))
                {
                    int[] tempArray = new int[5];
                    _packColorBalance.CopyTo(tempArray, 0);
                    int count = 0;

                    foreach (int colorVal in tempArray)
                    {
                        if (c.ManaRelation.ColorIdentity[count] > 0)
                        {
                            tempArray[count]++;
                        }
                        count++;
                    }
                    if (tempArray.Max() <= tempArray.Min() + 2)
                    {
                        tempArray.CopyTo(_packColorBalance, 0);
                        CardsInPack.Add(c);
                    }
                }
                infiniteLoopKiller++;
                if (infiniteLoopKiller > 100)
                {
                    CardsInPack.RemoveAll(x => x.CardRarity == "Uncommon");
                    infiniteLoopKiller = 0;
                }
            }
        }

        #endregion Private Methods
    }
}
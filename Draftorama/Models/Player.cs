using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draftorama.Models
{
    public class Player
    {
        #region Public Constructors

        public Player()
        {
            CardPool = new Pool();
            PackInHand = new Pack();
        }

        #endregion Public Constructors

        #region Public Properties

        public Pool CardPool { get; set; }
        public Pack PackInHand { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void MakePick(string pickIn)
        {
            Pick(PackInHand.CardsInPack.IndexOf(PackInHand.CardsInPack.First(a => a.CardName == pickIn)));
        }

        #endregion Public Methods

        #region Internal Methods

        internal void MakePick()
        {
            Random r = new Random();
            int pick = r.Next(PackInHand.NumCardsInPack);
            Pick(pick);
        }

        #endregion Internal Methods

        #region Private Methods

        private void Pick(int pickIn)
        {
            CardPool.PicksNotInDeck.Add(PackInHand.CardsInPack.ElementAt(pickIn));
            PackInHand.CardsInPack.RemoveAt(pickIn);
        }

        #endregion Private Methods
    }
}
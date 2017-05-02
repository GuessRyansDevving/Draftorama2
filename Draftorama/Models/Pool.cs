using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draftorama.Models
{
    public class Pool
    {
        #region Public Constructors

        public Pool()
        {
            PicksInDeck = new List<Card>();
            PicksNotInDeck = new List<Card>();
        }

        #endregion Public Constructors

        #region Public Properties

        public List<Card> PicksInDeck { get; set; }

        public List<Card> PicksNotInDeck { get; set; }

        #endregion Public Properties
    }
}
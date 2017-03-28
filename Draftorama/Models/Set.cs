using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<Card> GetAllCards()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draftorama.Models
{
    public class Pack : IPool
    {
        #region Constructors

        public Pack()
        {
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<Card> GetAllCards()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draftorama.Models
{
    public interface IPool
    {
        #region Methods

        IEnumerable<Card> GetAllCards();

        #endregion Methods
    }
}
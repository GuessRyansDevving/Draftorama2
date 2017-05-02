using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draftorama.Models
{
    public interface IDraftRepository
    {
        #region Public Methods

        void AddDraft(Draft draft);

        IEnumerable<Draft> GetAllDrafts();

        Draft GetDraftByName(string draftName);

        Task<bool> SaveChangesAsync();

        #endregion Public Methods
    }
}
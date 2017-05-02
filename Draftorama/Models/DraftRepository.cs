using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draftorama.Models
{
    public class DraftRepository : IDraftRepository
    {
        #region Private Fields

        private DraftContext _context;

        #endregion Private Fields

        #region Public Constructors

        public DraftRepository(DraftContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public void AddDraft(Draft draft)
        {
            _context.Add(draft);
        }

        public IEnumerable<Draft> GetAllDrafts()
        {
            return _context.Drafts.ToList();
        }

        public Draft GetDraftByName(string draftName)
        {
            return _context.Drafts.Where(d => d.DraftName == draftName).FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        #endregion Public Methods
    }
}
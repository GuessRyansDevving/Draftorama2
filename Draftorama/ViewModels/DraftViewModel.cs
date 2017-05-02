using System;
using System.Collections.Generic;
using System.Linq;

namespace Draftorama.ViewModels
{
    public class DraftViewModel
    {
        #region Public Properties

        public DateTime DraftDate { get; set; }

        public string DraftName { get; set; }

        public string SetName { get; set; }

        public string UserName { get; set; }

        #endregion Public Properties
    }
}
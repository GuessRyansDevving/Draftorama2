using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Draftorama.Models
{
    public class DraftContext : DbContext
    {
        #region Private Fields

        private IConfigurationRoot _config;

        #endregion Private Fields

        #region Public Constructors

        public DraftContext(IConfigurationRoot config, DbContextOptions options)
            : base(options)
        {
            _config = config;
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<Draft> Drafts { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DraftContextConnection"]);
        }

        #endregion Protected Methods
    }
}
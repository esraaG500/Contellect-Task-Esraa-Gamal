using Contellect.ContactApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contellect.ContactApp.DB.AppContext
{
    public class ContellectContext : DbContext
    {
        public ContellectContext(DbContextOptions<ContellectContext> options) : base(options) { }
        public DbSet<Contacts> Contacts { get; set; }
    }
}

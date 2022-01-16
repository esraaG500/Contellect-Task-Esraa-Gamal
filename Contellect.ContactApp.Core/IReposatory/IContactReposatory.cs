using Contellect.ContactApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contellect.ContactApp.Core.IReposatory
{
    public interface IContactReposatory
    {
        IQueryable<Contacts> GetAllContatct();
        Contacts GetByID(int id);

        bool AddContatct(Contacts contact);
        bool EditContatct(Contacts contact);
        bool UpdateRang(IQueryable<Contacts> contact);

        bool DeleteContatct(int id);

    }
}

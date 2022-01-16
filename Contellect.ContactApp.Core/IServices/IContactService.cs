using Contellect.ContactApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contellect.ContactApp.Core.IServices
{
    public interface IContactService
    {
        IQueryable<Contacts> GetAllContatct();
        IQueryable<Contacts> FilterContatct(Contacts contatct);
        Contacts GetByID(int id);
        bool SaveContatct(Contacts contatct);
        bool RemoveStatusUpdatedRow(int userCode);
        bool DeleteContatct(int id);
    }
}

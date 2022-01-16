using Contellect.ContactApp.Core.Entities;
using Contellect.ContactApp.Core.IReposatory;
using Contellect.ContactApp.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contellect.ContactApp.Service
{
    public class ContactService : IContactService
    {
        IContactReposatory _ContactReposatory;
        public ContactService(IContactReposatory contactReposatory)
        {
            _ContactReposatory = contactReposatory;
        }

        public IQueryable<Contacts> GetAllContatct()
        {
            try
            {
                var result = _ContactReposatory.GetAllContatct();
                return result;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool SaveContatct(Contacts contatct)
        {
            //add and edit
            //edit
            if (contatct.ContactID > 0)
            {
                if (_ContactReposatory.EditContatct(contatct))
                    return true;
                else
                    return false;
            }
            else  //add
            {
                if (_ContactReposatory.AddContatct(contatct))
                    return true;
                else
                    return false;
            }          
        }

        public bool DeleteContatct(int id)
        {
            if (_ContactReposatory.DeleteContatct(id))
                return true;
            else
                return false;
        }

        public Contacts GetByID(int id)
        {
            Contacts contact = new Contacts();
            if (id > 0)
                contact = _ContactReposatory.GetByID(id);
            return contact;
        }

        public IQueryable<Contacts> FilterContatct(Contacts contatct)
        {
            try
            {
                var result = _ContactReposatory.GetAllContatct();

                if (!String.IsNullOrEmpty(contatct.ContactName))
                    result = result.Where(x => x.ContactName.StartsWith(contatct.ContactName));
                if (!String.IsNullOrEmpty(contatct.ContactPhone))
                    result = result.Where(x => x.ContactName.StartsWith(contatct.ContactPhone));
                if (!String.IsNullOrEmpty(contatct.ContactAddress))
                    result = result.Where(x => x.ContactName.StartsWith(contatct.ContactAddress));
                if (!String.IsNullOrEmpty(contatct.ContactNotes))
                    result = result.Where(x => x.ContactName.StartsWith(contatct.ContactNotes));
                return result;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool RemoveStatusUpdatedRow(int userCode)
        {
            var result = _ContactReposatory.GetAllContatct().Where(x=>x.UserUpdatedID == userCode);
            foreach(var item in result)
            {
                item.UserUpdatedID = 0;
            }
            return _ContactReposatory.UpdateRang(result);
        }
    }
}

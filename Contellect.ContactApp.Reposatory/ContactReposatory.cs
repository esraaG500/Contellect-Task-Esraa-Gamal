using Contellect.ContactApp.Core.Entities;
using Contellect.ContactApp.Core.IReposatory;
using Contellect.ContactApp.DB.AppContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contellect.ContactApp.Reposatory
{
    public class ContactReposatory : IContactReposatory
    {
        ContellectContext _context;
        public ContactReposatory(ContellectContext context)
        {
            _context = context;
        }

        public IQueryable<Contacts> GetAllContatct() => _context.Contacts;

        public bool AddContatct(Contacts contatct)
        {
            try
            {
                _context.Contacts.Add(contatct);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        }
   
        public bool EditContatct(Contacts contatct)
        {
            try
            {
                var result = _context.Contacts.Find(contatct.ContactID);
                result.ContactName = contatct.ContactName;
                result.ContactPhone = contatct.ContactPhone;
                result.ContactAddress = contatct.ContactAddress;
                result.ContactNotes = contatct.ContactNotes;
                result.UserUpdatedID = contatct.UserUpdatedID;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public bool DeleteContatct(int id)
        {
            try
            {
                var contact = _context.Contacts.Find(id);
                _context.Remove(contact);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public Contacts GetByID(int id) => _context.Contacts.Find(id);

        public bool UpdateRang(IQueryable<Contacts> contact)
        {
            try
            {
                _context.UpdateRange(contact);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}

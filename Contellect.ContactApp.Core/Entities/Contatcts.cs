using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Contellect.ContactApp.Core.Entities
{
   public class Contacts
    {
        [Key]
        public int ContactID { get; set; }

        [Required]
        [StringLength(300)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "The Contact Phone Number cannot exceed 11 Number. ")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter valid integer Number")]
        public string ContactPhone { get; set; }

        [Required]
        [StringLength(300)]
        public string ContactAddress { get; set; }

        [MaxLength(4000)]
        public string ContactNotes { get; set; }
        public int UserUpdatedID { get; set; }
    }
}

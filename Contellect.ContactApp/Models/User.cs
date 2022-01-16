using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contellect.ContactApp.Models
{
    public class User
    {
       
        public int userCode { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string userPassword { get; set; }
    }
}

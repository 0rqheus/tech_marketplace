using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; } = 2;

        [Required(ErrorMessage = "Name isn't specified")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email isn't specified")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone isn't specified")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password isn't specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.MultilineText)] 
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public bool IsBanned { get; set; }

        public bool IsAdmin()
        {
            if (RoleId == 1) return true;
            return false;
        }
    }
}

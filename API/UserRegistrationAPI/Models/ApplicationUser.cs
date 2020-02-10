using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Column(TypeName ="nvarchar(150)")]
        public string FullName { get; set; }

        public int RoleId { get; set; }

        //public string UserName { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        //public string FullName { get; set; }
        [ForeignKey("RoleId")]
        
        public RoleType RoleType { get; set; }

    }
}

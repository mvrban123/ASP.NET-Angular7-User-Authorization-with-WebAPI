using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationAPI.Models
{
    public class RoleType
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}

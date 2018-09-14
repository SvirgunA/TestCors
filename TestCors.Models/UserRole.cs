using System;
using System.Collections.Generic;
using System.Text;
using TestCors.Common.Enums;

namespace TestCors.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public EUserRoles Role {get; set;}
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

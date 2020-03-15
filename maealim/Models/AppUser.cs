using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

        public Employee Employee { get; set; }
        public IEnumerable<MessageSend> MessageSends { get; set; }

        public IEnumerable<Attend> Attends { get; set; }

    }
}

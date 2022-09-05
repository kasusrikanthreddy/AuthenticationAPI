using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationManagement.API
{
    public class Executive
    {
        public int Id { get; set; }
        public string Experience { get; set; }
        public AppUser AppUser { get; set; }
    }
}
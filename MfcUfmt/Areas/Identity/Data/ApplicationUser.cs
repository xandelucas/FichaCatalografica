using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MfcUfmt.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
    }
}

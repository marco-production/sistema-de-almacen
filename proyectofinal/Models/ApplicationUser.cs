using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace proyectofinal.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual string CompletoNombre { get; set; }

    }
}

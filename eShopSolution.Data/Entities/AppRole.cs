using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class AppRole : IdentityRole<string>
    {
        public string Description { get; set; }
    }
}

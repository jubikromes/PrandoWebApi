using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PrandoWebServices.Data.Abstractions;
using PrandoWebServices.Data.Entities;
using PrandoWebServices.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PrandoWebServices.DbContext
{
    public class PrandoDbContext : IdentityDbContext<AppUser>
    {
        public PrandoDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var types = Assembly.GetAssembly(GetType()).GetTypes().Where(p =>
                typeof(IParentEntity).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);
            foreach (var type in types)
            {
                builder.Entity(type);
            }
        }
    }
}

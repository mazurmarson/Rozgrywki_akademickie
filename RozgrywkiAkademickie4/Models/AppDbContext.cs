using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Kierunek> Kierunki { get; set; }
        public DbSet<Sport> Sporty { get; set; }

        public DbSet<Zawody> Zawody { get; set; }

        public DbSet<Wynik> Wyniki { get; set; }

    }
}

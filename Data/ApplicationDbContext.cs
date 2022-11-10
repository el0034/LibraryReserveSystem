using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryReservedSystem.Models;

namespace LibraryReservedSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LibraryReservedSystem.Models.Book> Book { get; set; }
        public DbSet<LibraryReservedSystem.Models.UserProfile> UserProfile { get; set; }
        public DbSet<LibraryReservedSystem.Models.Role> Role { get; set; }
    }
}
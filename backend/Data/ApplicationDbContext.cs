using KichBackendApp.Models;

namespace KichBackendApp.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
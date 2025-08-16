using EduAtendance.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduAtendance.WebAPI.Context;

public sealed class ApplicationDbContext: DbContext 
{

    public ApplicationDbContext(DbContextOptions options):base(options)
    {
        
    }




    public DbSet<Student> Students { get; set; }
}

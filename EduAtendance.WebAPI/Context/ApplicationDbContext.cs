using EduAtendance.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduAtendance.WebAPI.Context;

public sealed class ApplicationDbContext: DbContext 
{

    public ApplicationDbContext(DbContextOptions options):base(options)
    {
        
    }



    public DbSet<SurveyTemplate> SurveyTemplates { get; set; }
    public DbSet<Student> Students { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SurveyTemplate>(opt =>
        {
            opt.OwnsMany(s => s.Categories, cb =>
            {
                cb.OwnsMany(c => c.Questions);
                cb.OwnsMany(c => c.Options);
            });
        });
    }
}

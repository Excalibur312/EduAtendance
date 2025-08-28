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

    public DbSet<MenuTemplate> MenuTemplates { get; set; }



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

 
        // MenuTemplate
        modelBuilder.Entity<MenuTemplate>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();

            entity.HasMany(x => x.Submenus)
                  .WithOne()
                  .HasForeignKey("MenuTemplateId")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Submenu (self-referencing)
        modelBuilder.Entity<MenuTemplateSubmenu>(entity =>
        {
            entity.HasKey(x => x.Title); // Title tekil olmalı veya Id ekleyebilirsin

            entity.HasMany(x => x.Submenus)
                  .WithOne()
                  .HasForeignKey("ParentId")
                  .OnDelete(DeleteBehavior.Restrict);

            // Items → string listesi yerine ayrı entity önerilir
            entity.OwnsMany(x => x.Items, items =>
            {
                items.WithOwner().HasForeignKey("SubmenuId");
                items.Property(i => i.Name).IsRequired();
            });
        });
    }

}
 
using Microsoft.EntityFrameworkCore; 
namespace EduAttendance.Benchmark
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
 
    }
}

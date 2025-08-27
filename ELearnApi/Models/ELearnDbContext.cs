using Microsoft.EntityFrameworkCore;

namespace ELearnApi.Models
{
    public class ELearnDbContext : DbContext
    {
        public ELearnDbContext(DbContextOptions<ELearnDbContext> options) : base(options)
        {

        }
        public DbSet<Feedback> feedback { get; set; }
        public DbSet<BookMarks> BookMarks
        { get; set; }
        public DbSet<Courses> Courses
        { get; set; }
        public DbSet<Reports> Reports
        { get; set; }
        
        public DbSet<Users> Users
        { get; set; }
      
        public DbSet<Card> Cards
        { get; set; }
    }
}
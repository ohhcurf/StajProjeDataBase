using Microsoft.EntityFrameworkCore;

namespace StajProjeDataBase.Database
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options) { }


        public DbSet<Students> Students { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<StudentSubjects> StudentSubjects { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<TeacherSubjects> TeacherSubjects { get; set; }
    }
}

namespace TextEmAll.DataAccess
{
    using TextEmAll.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class Challenge1DbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public Challenge1DbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Departments 
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Students 
        /// </summary>
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// Instructors 
        /// </summary>
        public DbSet<Instructor> Instructors { get; set; }

        /// <summary>
        /// Courses as a whole (non-specific to the underlying concrete type)
        /// </summary>
        public DbSet<Course> Courses { get; set; }
        
        /// <summary>
        /// Online courses 
        /// </summary>
        public DbSet<OnlineCourse> OnlineCourses { get; set; }

        /// <summary>
        /// Onsite courses 
        /// </summary>
        public DbSet<OnsiteCourse> OnsiteCourses { get; set; }

        /// <summary>
        /// Course course instructors 
        /// </summary>
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
        
        /// <summary>
        /// Access to course enrollments (student grades)
        /// I changed this because sometimes we want a legacy implementaton 
        /// in the database to have a more logical implementation name in the 
        /// application layer. Typically, I would coincide change to update 
        /// the offending name so that they are the same in both the RDBMS and 
        /// the app layer. Just showing the conceptual difference here 
        /// </summary>
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }

        /// <summary>
        /// Handle our configuration 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // set our connection string to SQL server 
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Challenge1Database"));

            // as a best practice, unless I'm outright changing the behavior of a method, when overriding, 
            // I like to ensure I'm calling into the base 
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// handling all of the non-attribute oriented configuration. Personally, I do not like 
        /// separating the configuration where some is done in one way and the remaining in another. 
        /// I left it like this as a way to demonstrate an understanding of multiple methods in the 
        /// case there is a development standard that provides guidance either way 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set up Person table-per-hierarchy polymorphism
            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Student>("Student")
                .HasValue<Instructor>("Instructor");

            // set up Course table-per-class polymorphism
            modelBuilder.Entity<OnlineCourse>().ToTable("OnlineCourse");
            modelBuilder.Entity<OnsiteCourse>().ToTable("OnsiteCourse");

            // set up Instructor one-to-one relationship with Office Assignment 
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.OfficeAssignment)
                .WithOne(oa => oa.Instructor)
                .HasForeignKey<OfficeAssignment>(oa => oa.InstructorId);

            // set up Instructor/Course many-to-many relationship 
            modelBuilder.Entity<CourseInstructor>()
                .HasKey(ci => new { ci.CourseId, ci.PersonId });
            modelBuilder.Entity<CourseInstructor>()
                .HasOne(ci => ci.Course)
                .WithMany(c => c.Instructors)
                .HasForeignKey(ci => ci.CourseId);
            modelBuilder.Entity<CourseInstructor>()
                .HasOne(ci => ci.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(ci => ci.PersonId);

            // set up Student/Course many-to-many relationship 
            modelBuilder.Entity<CourseEnrollment>()
                .HasKey(ce => new { ce.CourseId, ce.StudentId});
            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(ce => ce.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(ce => ce.CourseId);
            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(ce => ce.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(ce => ce.StudentId);

            // make sure we call the base method 
            base.OnModelCreating(modelBuilder);
        }
    }
}

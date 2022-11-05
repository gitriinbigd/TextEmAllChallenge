using System.ComponentModel.DataAnnotations.Schema;

namespace TextEmAll.Domain
{
    /// <summary>
    /// Many-to-many object joining our courses and instructors 
    /// </summary>
    [Table("CourseInstructor")]
    public class CourseInstructor
    {
        public int PersonId { get; set; }
        public Instructor Instructor { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}

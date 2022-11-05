namespace TextEmAll.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// A many-to-many table joining our students to courses 
    /// </summary>
    [Table("StudentGrade")]
    public class CourseEnrollment
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public decimal? Grade { get; set; }
    }
}

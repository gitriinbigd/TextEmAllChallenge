namespace TextEmAll.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Course")]
    public abstract class Course
    {
        [Key]
        public int CourseId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public int Credits { get; set; }
        
        [Required]
        public int DepartmentId { get; set; }
 
        public Department Department { get; set; }
        public ICollection<CourseInstructor> Instructors { get; set; }
        public virtual ICollection<CourseEnrollment> Students { get; set; }

    }
}

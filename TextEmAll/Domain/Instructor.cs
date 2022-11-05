namespace TextEmAll.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Instructor : Person
    {
        public virtual DateTime HireDate { get; set; }

        public ICollection<CourseInstructor> Courses { get; set; }
        
        public OfficeAssignment OfficeAssignment { get; set; }
    }
}

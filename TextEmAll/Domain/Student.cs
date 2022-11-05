namespace TextEmAll.Domain
{
    using System;
    using System.Collections.Generic;

    public class Student : Person
    {
        public virtual DateTime EnrollmentDate { get; set; }
        public virtual ICollection<CourseEnrollment> Courses { get; set; }
    }
}

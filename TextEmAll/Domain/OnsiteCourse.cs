namespace TextEmAll.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OnsiteCourse : Course
    {
        [MaxLength(50)]
        public string Location { get; set; }

        [MaxLength(50)]
        public string Days { get; set; }
        
        [Column(TypeName = "time")]
        public TimeSpan Time { get; set; }
    }
}

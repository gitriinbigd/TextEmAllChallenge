namespace TextEmAll.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OnlineCourse : Course
    {
        [MaxLength(100)]
        public string Url { get; set; }
    }
}

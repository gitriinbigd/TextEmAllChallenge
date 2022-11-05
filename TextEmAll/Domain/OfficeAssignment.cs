namespace TextEmAll.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("OfficeAssignment")]
    public class OfficeAssignment
    {
        [Key]
        [JsonIgnore]
        public int InstructorId { get; set; }
        
        [JsonIgnore]
        public Instructor Instructor { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}

namespace TextEmAll.Domain
{
    using System.ComponentModel.DataAnnotations;

    public abstract class Person
    {
        [Key]
        public virtual int PersonId { get; set; }
        
        [MaxLength(50)]
        public virtual string FirstName { get; set; }

        [MaxLength(50)]
        public virtual string LastName { get; set; }
    }
}

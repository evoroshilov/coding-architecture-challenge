using System.ComponentModel.DataAnnotations;

namespace VideoInstagram.DataLayer.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset LastModifiedDate { get; set; }

        public string LastModifiedBy { get; set; }
    }
}

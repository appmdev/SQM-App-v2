using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.Query.Domain.Entities
{
    [Table("Map")]
    public class MapEntity
    {
        [Key]
        public Guid MapId { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public string MapName { get; set; }
        public virtual ICollection<StateEntity> States { get; set; }
    }
}
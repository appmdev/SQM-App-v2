using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Query.Domain.Entities
{
    [Table("State")]
    public class StateEntity
    {
        [Key]
        public Guid MapId { get; set; }
        public Guid StateId { get; set; }
        public string RobotName { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }
        public string AdditionalData { get; set; }
        public DateTime StateDate { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual MapEntity Map { get; set; }
    }
}

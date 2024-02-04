using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.Query.Domain.Entities
{
    [Table("Pointcloud")]
    public class PointcloudEntity
    {
        [Key]
        public Guid PointcloudId { get; set; }
        public string RobotName { get; set; }
        public DateTime PointcloudDate { get; set; }
        public string Pointcloud { get; set; }
        public Guid MapId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual MapEntity Map { get; set; }
    }
}
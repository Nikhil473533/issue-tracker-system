using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTrackerAPI.Models
{
    [Table("audit_log")]
    public class AuditLog
    {
        [Key]
        public long id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string entity_name { get; set; } = string.Empty;

        [Required]
        public long entity_id { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string action { get; set; } = string.Empty;

        public string? before_state { get; set; }

        public string? after_state { get; set; }

        [Required]
        public DateTime event_time { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string audit_version { get; set; } = "1.0";
    }
}

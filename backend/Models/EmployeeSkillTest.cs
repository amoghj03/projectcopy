using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Models;

[Table("employee_skill_tests")]
public class EmployeeSkillTest
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("tenant_id")]
    public long TenantId { get; set; }

    [Column("employee_id")]
    public long EmployeeId { get; set; }

    [Column("skill_test_id")]
    public long? SkillTestId { get; set; }

    [Required]
    [Column("score")]
    public int Score { get; set; }

    [Required]
    [Column("max_score")]
    public int MaxScore { get; set; }

    [Column("percentage", TypeName = "decimal(5,2)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal Percentage { get; private set; }

    [Column("status")]
    [MaxLength(50)]
    public string? Status { get; set; }

    [Column("attempted_at")]
    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

    [Column("duration_minutes")]
    public int? DurationMinutes { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("TenantId")]
    public virtual Tenant Tenant { get; set; } = null!;

    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("SkillTestId")]
    public virtual SkillTest? SkillTest { get; set; }
}

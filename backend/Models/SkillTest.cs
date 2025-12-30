using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Models;

[Table("skill_tests")]
public class SkillTest
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("tenant_id")]
    public long TenantId { get; set; }

    [Required]
    [Column("title")]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [Column("skill_id")]
    public long? SkillId { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("total_questions")]
    public int? TotalQuestions { get; set; }

    [Column("passing_score")]
    public int? PassingScore { get; set; }

    [Column("duration_minutes")]
    public int? DurationMinutes { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("TenantId")]
    public virtual Tenant Tenant { get; set; } = null!;

    [ForeignKey("SkillId")]
    public virtual Skill? Skill { get; set; }
    
    public virtual ICollection<EmployeeSkillTest> EmployeeSkillTests { get; set; } = new List<EmployeeSkillTest>();
}

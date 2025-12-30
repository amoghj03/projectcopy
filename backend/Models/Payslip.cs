using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Models;

[Table("payslips")]
public class Payslip
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("tenant_id")]
    public long TenantId { get; set; }

    [Column("employee_id")]
    public long EmployeeId { get; set; }

    [Required]
    [Column("month")]
    public int Month { get; set; }

    [Required]
    [Column("year")]
    public int Year { get; set; }

    [Required]
    [Column("basic_salary", TypeName = "decimal(12,2)")]
    public decimal BasicSalary { get; set; }

    [Column("allowances", TypeName = "jsonb")]
    public string? Allowances { get; set; }

    [Column("deductions", TypeName = "jsonb")]
    public string? Deductions { get; set; }

    [Required]
    [Column("gross_salary", TypeName = "decimal(12,2)")]
    public decimal GrossSalary { get; set; }

    [Required]
    [Column("net_salary", TypeName = "decimal(12,2)")]
    public decimal NetSalary { get; set; }

    [Column("working_days")]
    public int? WorkingDays { get; set; }

    [Column("present_days")]
    public int? PresentDays { get; set; }

    [Column("leave_days", TypeName = "decimal(3,1)")]
    public decimal? LeaveDays { get; set; }

    [Column("status")]
    [MaxLength(50)]
    public string Status { get; set; } = "Generated";

    [Column("generated_by")]
    public long? GeneratedBy { get; set; }

    [Column("generated_at")]
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    [Column("sent_at")]
    public DateTime? SentAt { get; set; }

    [Column("acknowledged_at")]
    public DateTime? AcknowledgedAt { get; set; }

    [Column("pdf_url")]
    public string? PdfUrl { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("TenantId")]
    public virtual Tenant Tenant { get; set; } = null!;

    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("GeneratedBy")]
    public virtual Employee? GeneratedByEmployee { get; set; }
}

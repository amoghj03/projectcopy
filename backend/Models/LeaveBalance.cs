using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Models;

[Table("leave_balances")]
public class LeaveBalance
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("tenant_id")]
    public long TenantId { get; set; }

    [Column("employee_id")]
    public long EmployeeId { get; set; }

    [Column("leave_type_id")]
    public long LeaveTypeId { get; set; }

    [Required]
    [Column("year")]
    public int Year { get; set; }

    [Required]
    [Column("total_allocated", TypeName = "decimal(4,1)")]
    public decimal TotalAllocated { get; set; }

    [Column("used", TypeName = "decimal(4,1)")]
    public decimal Used { get; set; } = 0;

    [Column("pending", TypeName = "decimal(4,1)")]
    public decimal Pending { get; set; } = 0;

    [Column("available", TypeName = "decimal(4,1)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal Available { get; private set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("TenantId")]
    public virtual Tenant Tenant { get; set; } = null!;

    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("LeaveTypeId")]
    public virtual LeaveType LeaveType { get; set; } = null!;
}

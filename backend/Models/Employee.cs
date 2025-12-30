using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Models;

[Table("employees")]
public class Employee
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("tenant_id")]
    public long TenantId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Required]
    [Column("employee_id")]
    [MaxLength(50)]
    public string EmployeeId { get; set; } = string.Empty;

    [Required]
    [Column("full_name")]
    [MaxLength(255)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Column("email")]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Column("phone")]
    [MaxLength(50)]
    public string? Phone { get; set; }

    [Column("gender")]
    [MaxLength(50)]
    public string? Gender { get; set; }

    [Column("date_of_birth")]
    public DateOnly? DateOfBirth { get; set; }

    [Column("photo_url")]
    public string? PhotoUrl { get; set; }

    [Required]
    [Column("department")]
    [MaxLength(100)]
    public string Department { get; set; } = string.Empty;

    [Column("branch_id")]
    public long? BranchId { get; set; }

    [Column("job_role")]
    [MaxLength(100)]
    public string? JobRole { get; set; }

    [Column("status")]
    [MaxLength(50)]
    public string Status { get; set; } = "Active";

    [Required]
    [Column("join_date")]
    public DateOnly JoinDate { get; set; }

    [Column("salary", TypeName = "decimal(12,2)")]
    public decimal? Salary { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("emergency_contact")]
    [MaxLength(50)]
    public string? EmergencyContact { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("TenantId")]
    public virtual Tenant Tenant { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("BranchId")]
    public virtual Branch? Branch { get; set; }

    public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
    public virtual ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();
    public virtual ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
    public virtual ICollection<EmployeeSkillTest> EmployeeSkillTests { get; set; } = new List<EmployeeSkillTest>();
    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
    public virtual ICollection<TechIssue> TechIssues { get; set; } = new List<TechIssue>();
    public virtual ICollection<Payslip> Payslips { get; set; } = new List<Payslip>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

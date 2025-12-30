using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Models;

[Table("tenants")]
public class Tenant
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("slug")]
    [MaxLength(100)]
    public string Slug { get; set; } = string.Empty;

    [Column("domain")]
    [MaxLength(255)]
    public string? Domain { get; set; }

    [Column("subdomain")]
    [MaxLength(100)]
    public string? Subdomain { get; set; }

    [Column("logo_url")]
    public string? LogoUrl { get; set; }

    [Required]
    [Column("contact_email")]
    [MaxLength(255)]
    public string ContactEmail { get; set; } = string.Empty;

    [Column("contact_phone")]
    [MaxLength(50)]
    public string? ContactPhone { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("city")]
    [MaxLength(100)]
    public string? City { get; set; }

    [Column("state")]
    [MaxLength(100)]
    public string? State { get; set; }

    [Column("country")]
    [MaxLength(100)]
    public string? Country { get; set; }

    [Column("timezone")]
    [MaxLength(100)]
    public string Timezone { get; set; } = "UTC";

    [Column("currency")]
    [MaxLength(10)]
    public string Currency { get; set; } = "USD";

    [Column("settings", TypeName = "jsonb")]
    public string Settings { get; set; } = "{}";

    [Column("subscription_plan")]
    [MaxLength(50)]
    public string SubscriptionPlan { get; set; } = "basic";

    [Column("subscription_status")]
    [MaxLength(50)]
    public string SubscriptionStatus { get; set; } = "trial";

    [Column("subscription_expires_at")]
    public DateTime? SubscriptionExpiresAt { get; set; }

    [Column("max_employees")]
    public int MaxEmployees { get; set; } = 50;

    [Column("max_branches")]
    public int MaxBranches { get; set; } = 5;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("onboarded_at")]
    public DateTime OnboardedAt { get; set; } = DateTime.UtcNow;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

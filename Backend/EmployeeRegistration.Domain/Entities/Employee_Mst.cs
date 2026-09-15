using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeRegistration.Domain.Entities;

public class Employee_Mst
{
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    [MaxLength(30)]
    public string EmployeeName { get; set; } = string.Empty;

    public int Age { get; set; }

    [Required]
    [MaxLength(10)]
    public string MobileNum { get; set; } = string.Empty;

    [Required]
    [MaxLength(6)]
    public string Pincode { get; set; } = string.Empty;

    public DateTime? DOB { get; set; }

    [Required]
    [MaxLength(250)]
    public string AddressLine1 { get; set; } = string.Empty;

    [MaxLength(250)]
    public string AddressLine2 { get; set; } = string.Empty;

    public int StateId { get; set; }
    [ForeignKey("StateId")]
    public State_Mst? State { get; set; }

    public int CountryId { get; set; }
    [ForeignKey("CountryId")]
    public Country_Mst? Country { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeRegistration.Domain.Entities;

public class State_Mst
{
    [Key]
    public int StateId { get; set; }
    public string StateName { get; set; } = string.Empty;

    public int CountryId { get; set; }

    [ForeignKey("CountryId")]
    public Country_Mst? Country { get; set; }
}

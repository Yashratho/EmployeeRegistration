using System.ComponentModel.DataAnnotations;

namespace EmployeeRegistration.Domain.Entities;

public class Country_Mst
{
    [Key]
    public int CountryId { get; set; }
    public string CountryName { get; set; } = string.Empty;

    public ICollection<State_Mst> States { get; set; } = new List<State_Mst>();
}

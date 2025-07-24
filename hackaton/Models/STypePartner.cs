//Выполнил Арнаутов 22.07.2025
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("s_type_partner")]
public class STypePartner
{
    public int id { get; set; }
    public string type { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<General> general { get; set; }
} 
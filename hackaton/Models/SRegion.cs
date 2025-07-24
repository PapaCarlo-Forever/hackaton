//Выполнил Арнаутов 22.07.2025
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("s_region")]
public class SRegion
{
    public int id { get; set; }

    public string region { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<General> general { get; set; }
} 
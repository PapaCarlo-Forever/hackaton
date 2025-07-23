using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("s_nomenclature")]
public class SNomenclature
{

    public int id { get; set; }

    public string name { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<General> general { get; set; }
} 
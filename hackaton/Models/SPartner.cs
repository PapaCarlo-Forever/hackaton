using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("s_partner")]
public class SPartner
{
    public int id { get; set; }
    public long INN { get; set; }
    public string name { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<General> general { get; set; }
} 
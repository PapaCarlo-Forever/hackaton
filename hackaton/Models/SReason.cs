using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("s_reason")]
public class SReason
{
    public int id { get; set; }
    public string reason { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<General> general { get; set; }
} 
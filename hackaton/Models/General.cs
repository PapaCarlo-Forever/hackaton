//Выполнил Арнаутов 22.07.2025
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("general")]
public class General
{
    public int id { get; set; }
    public int month { get; set; }
    public int year { get; set; }
    public decimal weight { get; set; }

    [Column("s_partner_id")]
    public int s_partner_id { get; set; }

    [Column("s_manager_id")]
    public int s_manager_id { get; set; }

    [Column("s_type_partner_id")]
    public int s_type_partner_id { get; set; }

    [Column("s_nomenclature_id")]
    public int s_nomenclature_id { get; set; }

    [Column("s_region_id")]
    public int s_region_id { get; set; }

    [Column("s_reason_id")]
    public int? s_reason_id { get; set; }

    // ������������� ��������
    [ForeignKey("s_partner_id")]
    public SPartner SPartner { get; set; }
    [ForeignKey("s_manager_id")]
    public SManager SManager { get; set; }
    [ForeignKey("s_type_partner_id")]
    public STypePartner STypePartner { get; set; }
    [ForeignKey("s_nomenclature_id")]
    public SNomenclature SNomenclature { get; set; }
    [ForeignKey("s_region_id")]
    public SRegion SRegion { get; set; }
    [ForeignKey("s_reason_id")]
    public SReason SReason { get; set; }
}
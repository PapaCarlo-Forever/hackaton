//Выполнил Арнаутов 22.07.2025
using Microsoft.EntityFrameworkCore;

public class GeneralDbContext : DbContext
{
    public GeneralDbContext(DbContextOptions<GeneralDbContext> options) : base(options) { }

    public DbSet<General> General { get; set; }
    public DbSet<SPartner> SPartner { get; set; }
    public DbSet<SManager> SManager { get; set; }
    public DbSet<STypePartner> STypePartner { get; set; }
    public DbSet<SNomenclature> SNomenclature { get; set; }
    public DbSet<SReason> SReason { get; set; }
    public DbSet<SRegion> SRegion { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<General>().ToTable("general");
        modelBuilder.Entity<SPartner>().ToTable("s_partner");
        modelBuilder.Entity<SManager>().ToTable("s_manager");
        modelBuilder.Entity<STypePartner>().ToTable("s_type_partner");
        modelBuilder.Entity<SNomenclature>().ToTable("s_nomenclature");
        modelBuilder.Entity<SReason>().ToTable("s_reason");
        modelBuilder.Entity<SRegion>().ToTable("s_region");

        modelBuilder.Entity<General>(entity =>
        {
            entity.HasKey(g => g.id);
            entity.Property(g => g.weight).HasColumnType("decimal(6,3)");
            entity.HasOne(g => g.SPartner)
                .WithMany(p => p.general)
                .HasForeignKey(g => g.s_partner_id);
            entity.HasOne(g => g.SManager)
                .WithMany(m => m.general)
                .HasForeignKey(g => g.s_manager_id);
            entity.HasOne(g => g.STypePartner)
                .WithMany(t => t.general)
                .HasForeignKey(g => g.s_type_partner_id);
            entity.HasOne(g => g.SNomenclature)
                .WithMany(n => n.general)
                .HasForeignKey(g => g.s_nomenclature_id);
            entity.HasOne(g => g.SRegion)
                .WithMany(r => r.general)
                .HasForeignKey(g => g.s_region_id);
            entity.HasOne(g => g.SReason)
                .WithMany(r => r.general)
                .HasForeignKey(g => g.s_reason_id);
        });
    }

}
using Microsoft.EntityFrameworkCore;

namespace DALEF.Models;

public partial class TradingCompanyModel: DbContext
{
    public TradingCompanyModel() { }
    public TradingCompanyModel(DbContextOptions<TradingCompanyModel> options)
        : base(options) { }
    public virtual DbSet<TblUserInfo> TblUsers { get; set; }
    public virtual DbSet<TblBankCard> TblBankCards { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("abracadabra");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBankCard>(entity =>
        {
            entity.HasKey(e => e.BankCardId);
            entity.ToTable("bank_card");
            entity.Property(e => e.Number).HasMaxLength(16);
            entity.Property(e => e.PIN).HasMaxLength(6);
            entity.Property(e => e.CVV).HasMaxLength(3);

            entity.HasOne(d => d.UserInfo)
            .WithMany(p => p.TblBankCards)
            .HasForeignKey(d => d.OwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_bank_card_user_info");
        });

        modelBuilder.Entity<TblUserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.ToTable("user_info");
            entity.Property(e => e.UserLogin).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.HashPassword).HasMaxLength(82);
            entity.Property(e => e.PasswordKeyword).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.UserAddress).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

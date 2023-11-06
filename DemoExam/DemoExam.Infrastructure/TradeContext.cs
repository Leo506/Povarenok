using Microsoft.EntityFrameworkCore;

namespace DemoExam.Infrastructure;

internal partial class TradeContext : DbContext
{
    public TradeContext()
    {
        Database.EnsureCreated();
    }

    public TradeContext(DbContextOptions<TradeContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PickupPoint> PickupPoints { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Trade;User Id=sa;Password=mssql1Ipw;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Manufacturer_pk");

            entity.ToTable("Manufacturer");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__C3905BAFA031FC03");

            entity.ToTable("Order");

            entity.HasIndex(e => e.PickupPointId, "IX_Order_OrderPickupPoint");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.OrderPickupPointNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PickupPointId)
                .HasConstraintName("Order_PickupPoint_null_fk");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Order_User_UserID_fk");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderItem_pk");

            entity.ToTable("OrderItem");

            entity.HasIndex(e => e.OrderId, "IX_OrderList_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_OrderList_ProductId");

            entity.Property(e => e.ProductId).HasMaxLength(100);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("OrderList_Order_null_fk");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("OrderList_Product_null_fk");
        });

        modelBuilder.Entity<PickupPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PickupPoint_pk");

            entity.ToTable("PickupPoint");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.PostIndex)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Street).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__Product__2EA7DCD5B04CBEB8");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ManufacturerId, "IX_Product_ManufacturerId");

            entity.HasIndex(e => e.SupplierId, "IX_Product_SupplierId");

            entity.Property(e => e.ArticleNumber).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Photo).HasColumnType("image");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Manufacturer_Id_fk");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Supplier_Id_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__8AFACE3A03B089D8");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Supplier_pk");

            entity.ToTable("Supplier");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__1788CCACD83B2744");

            entity.ToTable("User");

            entity.HasIndex(e => e.RoleId, "IX_User_UserRole");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Patronymic).HasMaxLength(100);
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__UserRole__2F10007B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

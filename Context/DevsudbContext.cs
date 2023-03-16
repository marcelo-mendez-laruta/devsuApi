
using Microsoft.EntityFrameworkCore;

namespace devsuApi.Context;

public partial class DevsudbContext : DbContext
{
    private readonly string connectionString;
    public DevsudbContext(string _connectionString)
    {
        connectionString = _connectionString;
    }

    public DevsudbContext(DbContextOptions<DevsudbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Movement> Movements { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.AccountId, "AccountID").IsUnique();

            entity.HasIndex(e => e.ClientId, "ClientID");

            entity.Property(e => e.AccountId)
                .HasColumnType("int(11)")
                .HasColumnName("AccountID");
            entity.Property(e => e.Balance)
                .HasDefaultValueSql("'0'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.ClientId)
                .HasColumnType("int(11)")
                .HasColumnName("ClientID");
            entity.Property(e => e.Number).HasColumnType("int(11)");
            entity.Property(e => e.State).HasDefaultValueSql("'1'");
            entity.Property(e => e.Type).HasMaxLength(255);

            entity.HasOne(d => d.Client).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("accounts_ibfk_1");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PRIMARY");

            entity.ToTable("clients");

            entity.HasIndex(e => e.ClientId, "ClientID").IsUnique();

            entity.HasIndex(e => e.PersonId, "PersonID").IsUnique();

            entity.Property(e => e.ClientId)
                .HasColumnType("int(11)")
                .HasColumnName("ClientID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PersonId)
                .HasColumnType("int(11)")
                .HasColumnName("PersonID");
            entity.Property(e => e.State).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.Person).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.PersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("clients_ibfk_1");
        });

        modelBuilder.Entity<Movement>(entity =>
        {
            entity.HasKey(e => e.MovementId).HasName("PRIMARY");

            entity.ToTable("movements");

            entity.HasIndex(e => e.AccountId, "AccountID");

            entity.HasIndex(e => e.MovementId, "MovementID").IsUnique();

            entity.Property(e => e.MovementId)
                .HasColumnType("int(11)")
                .HasColumnName("MovementID");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(11)")
                .HasColumnName("AccountID");
            entity.Property(e => e.Amount).HasColumnType("bigint(20)");
            entity.Property(e => e.Balance).HasColumnType("bigint(20)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);

            entity.HasOne(d => d.Account).WithMany(p => p.Movements)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("movements_ibfk_1");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PRIMARY");

            entity.ToTable("persons");

            entity.HasIndex(e => e.PersonId, "PersonID").IsUnique();

            entity.Property(e => e.PersonId)
                .HasColumnType("int(11)")
                .HasColumnName("PersonID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Age).HasColumnType("int(11)");
            entity.Property(e => e.Gender).HasMaxLength(255);
            entity.Property(e => e.Identification).HasMaxLength(255);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("NAME");
            entity.Property(e => e.Phone).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

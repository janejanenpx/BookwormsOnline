
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookwormsOnline_231660A.Model
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AuditLog> AuditLogs { get; set; }

        private readonly IConfiguration _configuration;

        //public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
        //public AuthDbContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string connectionString = _configuration.GetConnectionString(name: "AuthConnectionString");
        //    optionsBuilder.UseSqlServer(connectionString);
        //}


        public AuthDbContext(DbContextOptions<AuthDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AuthConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                // Keep existing configurations and add:
                entity.Property(u => u.EncryptedCreditCard)
                    .HasMaxLength(500) // Add length restriction for encrypted data
                    .IsRequired();
            });
        }

        // Override OnModelCreating to explicitly map the custom properties.
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    // Map ApplicationUser extra properties so that their values are saved.
        //    builder.Entity<ApplicationUser>(entity =>
        //    {
        //        entity.Property(u => u.FirstName)
        //              .HasMaxLength(50)
        //              .IsRequired();

        //        entity.Property(u => u.LastName)
        //              .HasMaxLength(50)
        //              .IsRequired();

        //        entity.Property(u => u.MobileNumber)
        //              .IsRequired();

        //        entity.Property(u => u.EncryptedCreditCard)
        //              .IsRequired();

        //        entity.Property(u => u.BillingAddress)
        //              .HasMaxLength(200)
        //              .IsRequired();

        //        entity.Property(u => u.ShippingAddress)
        //              .HasMaxLength(200)
        //              .IsRequired();

        //        // PhotoPath can be left as optional.
        //        entity.Property(u => u.PhotoPath);
        //    });
        //}

        public class AuditLog
        {
            public int Id { get; set; }
            public string UserId { get; set; }
            public string Activity { get; set; }
            public DateTime Timestamp { get; set; }
            public string IpAddress { get; set; }
        }
    }
}

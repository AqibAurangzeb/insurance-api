using Microsoft.EntityFrameworkCore;

namespace Insurance.DataAccess
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Domain.Entities.Claims> Claims { get; set; }
        public DbSet<Domain.Entities.ClaimType> ClaimType { get; set; }
        public DbSet<Domain.Entities.Company> Company { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Claims>(entity =>
            {
                entity.ToTable("Claims");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ucr)
                    .HasColumnName("UCR")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyId");

                entity.Property(e => e.ClaimDate)
                    .HasColumnName("ClaimDate");

                entity.Property(e => e.LossDate)
                    .HasColumnName("LossDate");

                entity.Property(e => e.AssuredName)
                    .HasColumnName("AssuredName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IncurredLoss)
                    .HasPrecision(15, 2)
                    .HasColumnName("IncurredLoss");


                entity.Property(e => e.Closed)
                    .HasColumnName("Closed");
            });

            modelBuilder.Entity<Domain.Entities.ClaimType>(entity =>
            {
                entity.ToTable("ClaimType");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Domain.Entities.Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address1)
                    .HasColumnName("Address1")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasColumnName("Address2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address3)
                    .HasColumnName("Address3")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Postcode)
                    .HasColumnName("Postcode")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("Country")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Active)
                    .HasColumnName("Active");

                entity.Property(e => e.InsuranceEndDate)
                    .HasColumnName("InsuranceEndDate");
            });

            modelBuilder.Entity<Domain.Entities.Claims>()
                .HasData(
                    new Domain.Entities.Claims
                    {
                        Id = 1,
                        Ucr = "ABCD",
                        CompanyId = 1,
                        ClaimDate = new DateTime(2018, 05, 20),
                        LossDate = new DateTime(2018, 05, 20),
                        AssuredName = "Aqib A",
                        IncurredLoss = 2500,
                        Closed = true
                    }
                );

            modelBuilder.Entity<Domain.Entities.ClaimType>()
                .HasData(
                    new Domain.Entities.ClaimType
                    {
                        Id = 1,
                        Name = "Burglary and Theft"
                    },
                    new Domain.Entities.ClaimType
                    {
                        Id = 2,
                        Name = "Auto Accident"
                    },
                    new Domain.Entities.ClaimType
                    {
                        Id = 3,
                        Name = "Fire"
                    }
                );

            modelBuilder.Entity<Domain.Entities.Company>()
                .HasData(
                    new Domain.Entities.Company
                    {
                        Id = 1,
                        Name = "Aqib LTD",
                        Address1 = "24 Water Lane",
                        Address2 = "25 Water Lane",
                        Address3 = "26 Water Lane",
                        Postcode = "LS5 5ST",
                        Country = "UK",
                        Active= true,
                        InsuranceEndDate = new DateTime(2023, 12, 28)
                    },
                    new Domain.Entities.Company
                    {
                        Id = 2,
                        Name = "Biqa LTD",
                        Address1 = "34 Fire Lane",
                        Address2 = "35 Fire Lane",
                        Address3 = "36 Fire Lane",
                        Postcode = "LS7 2ST",
                        Country = "Netherlands",
                        Active = false,
                        InsuranceEndDate = new DateTime(2022, 12, 28)
                    }
                );
        }

    }
}

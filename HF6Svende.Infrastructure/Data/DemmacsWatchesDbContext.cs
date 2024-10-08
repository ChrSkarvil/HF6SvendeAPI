﻿using HF6Svende.Core.Entities;
using HF6SvendeAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HF6SvendeAPI.Data
{
    public class DemmacsWatchesDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DemmacsWatchesDbContext()
        {
        }

        public DemmacsWatchesDbContext(DbContextOptions<DemmacsWatchesDbContext> options, IConfiguration config)
    : base(options)
        {
            _config = config;
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Listing> Listings { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PostalCode> PostalCodes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductColor> ProductColors { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        //Connection to db
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DemmacsWatches"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // INSERT DATA

            // INSERT INTO COUNTRY
            modelBuilder.Entity<Country>().HasData(
                new Country {Id = 1, Name = "Denmark", CountryCode = "DK" },
                new Country {Id = 2, Name = "Sweden", CountryCode = "SE" }
                );

            // INSERT INTO POSTALCODE
            modelBuilder.Entity<PostalCode>().HasData(
                new PostalCode {Id = 1, PostCode = "5610", City = "Assens", CountryId = 1 }
                );

            // INSERT INTO GENDER
            modelBuilder.Entity<Gender>().HasData(
                new Gender {Id = 1, Name = "Male"},
                new Gender {Id = 2, Name = "Female"}
                );

            // INSERT INTO ROLE
            modelBuilder.Entity<Role>().HasData(
                new Role {Id = 1, Name = "Admin" }
                );

            // INSERT INTO CATEGORY
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Watches", GenderId = 1 }
                );

            // INSERT INTO CUSTOMER
            modelBuilder.Entity<Customer>().HasData(
                new Customer {Id = 1, FirstName = "Christian", LastName = "Skarvil", Address = "Mimose 7", Phone = 32102918, Email = "cs@live.dk", PostalCodeId = 1, CountryId = 1 }
                );

            // INSERT INTO PAYMENT
            modelBuilder.Entity<Payment>().HasData(
                new Payment {Id = 1, Method = "Card", Amount = 1000, CustomerId = 1 }
                );

            // INSERT INTO Delivery
            modelBuilder.Entity<Delivery>().HasData(
                new Delivery {Id = 1, Address = "gogade 22", 
                    DispatchedDate = new DateTime(2024, 8, 16, 10, 9, 15, 433, DateTimeKind.Utc), 
                    EstDeliveryDate = new DateTime(2024, 8, 16, 10, 9, 15, 433, DateTimeKind.Utc),
                    DeliveredDate = new DateTime(2024, 8, 16, 10, 9, 15, 433, DateTimeKind.Utc),
                    DeliveryFee = 30,
                    CountryId = 1,
                    PostalCodeId = 1}
                );


            //CART
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Quantity)
                    .HasDefaultValue(1);

                entity.Property(e => e.CreateDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            //CATEGORY
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //COLOR
            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                .IsRequired();
            });

            //COUNTRY
            modelBuilder.Entity<Country>(entity =>
            {

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                .IsRequired();

                entity.Property(e => e.CountryCode)
                .IsRequired();
            });

            //CUSTOMER
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired();

                entity.Property(e => e.Address)
                    .IsRequired();

                entity.Property(e => e.Phone)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired();

                entity.Property(e => e.CreateDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PostalCode)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PostalCodeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //DELIVERY
            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Address)
                .IsRequired();

                entity.Property(e => e.DispatchedDate);

                entity.Property(e => e.EstDeliveryDate);

                entity.Property(e => e.DeliveredDate);

                entity.Property(e => e.DeliveryFee)
                .IsRequired();


                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PostalCode)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.PostalCodeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //DEPARTMENT
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                .IsRequired();

                entity.Property(e => e.Address)
                .IsRequired();

                entity.Property(e => e.Email)
                .IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PostalCode)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.PostalCodeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //EMPLOYEE
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired();

                entity.Property(e => e.Address)
                    .IsRequired();

                entity.Property(e => e.Phone)
                    .IsRequired();

                entity.Property(e => e.Salary)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired();

                entity.Property(e => e.HireDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.EndDate)
                    .HasDefaultValueSql("NULL");


                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PostalCode)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PostalCodeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //GENDER
            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired();
            });

            //IMAGE
            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.File)
                .IsRequired();

                entity.Property(e => e.CreateDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(d => d.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.IsVerified)
                    .HasDefaultValue(1);
            });

            //LISTING
            modelBuilder.Entity<Listing>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .IsRequired();

                entity.Property(e => e.CreateDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.ExpireDate);

                entity.Property(e => e.SoldDate);

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(1);

                entity.Property(e => e.DenyDate);

                entity.Property(e => e.DeleteDate);


                entity.HasOne(d => d.Product)
                    .WithOne(p => p.Listing)
                    .HasForeignKey<Listing>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //LOGIN
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired();

                entity.Property(e => e.UserType)
                    .HasConversion<int>();

                entity.HasOne(d => d.Customer)
                    .WithOne(p => p.Login)
                    .HasForeignKey<Login>(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.Login)
                    .HasForeignKey<Login>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.RefreshTokens)
                   .WithOne(p => p.Login)
                   .HasForeignKey(p => p.LoginId)
                   .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(1);
            });

            //ORDER
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CreateDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Delivery)
                    .WithOne(p => p.Order)
                    .HasForeignKey<Order>(d => d.DeliveryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //ORDERITEM
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Price)
                    .IsRequired();

                entity.Property(e => e.Quantity)
                    .HasDefaultValue(1);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //PAYMENT
            modelBuilder.Entity<Payment>(entity =>
            {

                entity.HasKey(e => e.Id);

                entity.Property(e => e.CreateDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Method)
                .IsRequired();

                entity.Property(e => e.Amount)
                .IsRequired();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //POSTALCODE
            modelBuilder.Entity<PostalCode>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PostCode)
                .IsRequired();

                entity.Property(e => e.City)
                .IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PostalCodes)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //PRODUCT
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Brand)
                .IsRequired();

                entity.Property(e => e.Description)
                .IsRequired();

                entity.Property(e => e.Size)
                .IsRequired();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //PRODUCTCOLOR
            modelBuilder.Entity<ProductColor>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ProductColors)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductColors)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //REFRESHTOKEN
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Token)
                    .IsRequired();

                entity.Property(e => e.ExpiresAt)
                    .IsRequired();

                entity.Property(e => e.RevokedAt);
            });

            //ROLE
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                .IsRequired();
            });

        }

    }
}

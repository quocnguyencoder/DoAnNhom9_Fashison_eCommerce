using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebAPI
{
    public partial class LTWeb : DbContext
    {
        public LTWeb()
            : base("name=LTWeb")
        {
        }

        public virtual DbSet<Authorize> Authorizes { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Cart_Item> Cart_Item { get; set; }
        public virtual DbSet<Main_Type> Main_Type { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orders_Item> Orders_Item { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_Type> Product_Type { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasMany(e => e.Cart_Item)
                .WithRequired(e => e.Cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Main_Type>()
                .HasMany(e => e.Product_Type)
                .WithOptional(e => e.Main_Type)
                .HasForeignKey(e => e.MaintypeID);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Orders_Item)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.Authorizes)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Cart_Item)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ItemID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders_Item)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_Type>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Product_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Authorizes)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.Address)
                .IsFixedLength();

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Store)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.User_ID);
        }
    }
}

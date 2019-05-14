namespace Speisekarte.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SQLContext : DbContext
    {
        public SQLContext()
            : base("name=SQLContext")
        {
        }

        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>()
                .HasMany(e => e.Menus)
                .WithMany(e => e.Drinks)
                .Map(m => m.ToTable("MenuDrinkRelation").MapLeftKey("DrinkID").MapRightKey("MenuID"));

            modelBuilder.Entity<Meal>()
                .HasMany(e => e.Menus)
                .WithMany(e => e.Meals)
                .Map(m => m.ToTable("MenuMealRelation").MapLeftKey("MealID").MapRightKey("MenuID"));

        }
    }
}

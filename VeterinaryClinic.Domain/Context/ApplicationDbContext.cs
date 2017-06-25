using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using VeterinaryClinic.Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VeterinaryClinic.Domain.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationPosition> OperationPositions { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetOperation> PetOperations { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Position> Positions { get; set; }

        public DbSet<Procurement> Procurements { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }


        public ApplicationDbContext()
            : base("VeterenaryClinicConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Отключаем каскадное удаление данных в связанных таблицах
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Запрещаем создание имен таблиц в множественном числе в т.ч. при связи многие к многим
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Procurement>()
            //    .HasRequired<Product>(s => s.Product)
            //    .WithMany(s => s.Procurements)
            //    .HasForeignKey(s => s.ProductId);

            //modelBuilder.Entity<Sale>()
            //    .HasRequired<Product>(s => s.Product)
            //    .WithMany(s => s.Sales)
            //    .HasForeignKey(s => s.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

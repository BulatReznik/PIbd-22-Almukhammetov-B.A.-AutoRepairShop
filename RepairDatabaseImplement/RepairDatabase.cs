using RepairDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace RepairDatabaseImplement
{
    public class RepairDatabase: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false) 
            {
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-UJ3H26AC\SQLEXPRESS;Initial Catalog=RepairDatabase;Integrated Security=True; MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Repair> Repairs { get; set; }
        public virtual DbSet<RepairComponent> RepairComponents { get; set; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
    }
}

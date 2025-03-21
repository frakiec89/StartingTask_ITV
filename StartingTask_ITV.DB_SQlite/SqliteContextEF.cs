using Microsoft.EntityFrameworkCore;
using StartingTask_ITV.DB_SQlite.Model;

namespace StartingTask_ITV.DB_SQlite
{
    internal class SqliteContextEF : DbContext
    {
        private readonly string _cs; // строка соедиения

        public SqliteContextEF(string cs) : base()
        {
            _cs = cs;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(_cs); 

        internal DbSet<Device> Devices {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()  
                .Property(d => d.Id)    // правка  - выключим  авто-инкремент  в связи с правкой  задания
                .ValueGeneratedNever(); 

            base.OnModelCreating(modelBuilder);
        }
    }
}

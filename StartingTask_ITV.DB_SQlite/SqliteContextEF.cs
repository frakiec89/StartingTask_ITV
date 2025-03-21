﻿using Microsoft.EntityFrameworkCore;
using StartingTask_ITV.DB_SQlite.Model;

namespace StartingTask_ITV.DB_SQlite
{
    internal class SqliteContextEF : DbContext
    {
        public SqliteContextEF() : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite("FileName=Devices.DB"); // todo вынести  в Configuration 

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

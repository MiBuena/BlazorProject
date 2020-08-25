using ListGenerator.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Api.DB
{
    public class ListGenerationContext : DbContext
    {
        public ListGenerationContext(DbContextOptions<ListGenerationContext> options)
            : base(options)
        {
        } 
        
        public DbSet<Item> Items { get; set; }

        public DbSet<Replenishment> Replenishments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PurchasedItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.PurchasedItems)
                .HasForeignKey(x => x.ItemId);
        }
    }
}

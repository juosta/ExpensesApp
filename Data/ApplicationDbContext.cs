using ExpensesApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ExpensesApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });
            builder.Entity<Role>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });
            builder.Entity<Transaction>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");

                b.HasOne(x => x.User)
                    .WithMany(x => x.Transactions)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(x => x.TransactionCategory)
                    .WithMany()
                    .HasForeignKey(x => x.TransactionCategoryId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<TransactionCategory>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");

                b.HasOne(x => x.User)
                    .WithMany(x => x.Categories)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Transaction> Transactions { get; internal set; }
        public DbSet<TransactionCategory> TransactionCategories { get; internal set; }
    }
}

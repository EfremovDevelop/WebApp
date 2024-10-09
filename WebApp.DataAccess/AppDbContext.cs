using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using WebApp.Domain;

namespace WebApp.DataAccess;

public class AppDbContext(
        DbContextOptions<AppDbContext> options)
        : DbContext(options)
{
    public virtual DbSet<UserStatisticRequest> UserStatisticRequests { get; set; }
    public virtual DbSet<Result> Results { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserStatisticRequest>().HasKey(u => u.Id);
        modelBuilder.Entity<Result>().HasKey(u => u.Id);

        base.OnModelCreating(modelBuilder);
    }
}

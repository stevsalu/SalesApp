﻿using Microsoft.EntityFrameworkCore;
using SalesApp.Server.Models;

namespace SalesApp.Server.Data;
public class AppDbContext : DbContext{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductCategory> Categories => Set<ProductCategory>();

}


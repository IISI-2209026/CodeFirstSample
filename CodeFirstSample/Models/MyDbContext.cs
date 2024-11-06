using System;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstSample;

public class MyDbContext : DbContext
{
    private static bool[] s_migrated = new bool[] { false };
    public DbSet<Person> Persons { get; set; }
    
    public MyDbContext(DbContextOptions options, bool isDesignTime = false) : base(options)
    {
        if (isDesignTime)
            return;
        
        
        // DbContext 自動 Migration 的策略與邏輯
        // 需要依照情境做調整
        if (!s_migrated[0])
        {
            lock (s_migrated)
            {
                if (!s_migrated[0])
                {
                    // Database Migration
                    Database.Migrate();
                    s_migrated[0] = true;
                }
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id);
            
            entity.ToTable("Person", op => 
                op.HasComment("This is a person table"));

            entity.Property(p => p.Id)
                .IsRequired()
                // 自動跳號
                .ValueGeneratedOnAdd();
            
            entity.Property(p => p.Name)
                .IsRequired()
                .HasComment("This is a person name")
                .HasMaxLength(30)
                .HasColumnType("varchar(30)");
            
            entity.Property(p => p.BirthDate)
                .IsRequired()
                .HasComment("This is a person birth date")
                // 指定欄位型別
                .HasColumnType("timestamp")
                ;
            
            entity.Property(p => p.CreateTime)
                .IsRequired()
                .HasComment("This is a create time")
                .HasColumnType("timestamp")
                // 指定預設值 SQL
                .HasDefaultValueSql("NOW()")
                ;
        });

        // 資料庫第一次建立時，填充的資料
        modelBuilder.Entity<Person>()
            .HasData([
                new Person()
                {
                    Id = 1,
                    Name = "John",
                    BirthDate = new DateTime(2000, 1, 1)
                },
                new Person()
                {
                    Id = 2,
                    Name = "Mary",
                    BirthDate = new DateTime(2001, 2, 2)
                }
            ]);
    }
}
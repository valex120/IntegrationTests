using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Marketplace.DataAccess;

/// <summary>
///     Контекст БД  маркетплейса
/// </summary>
public class MarketplaceContext : DbContext
{
    /// <summary>
    ///     Товары
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    ///     Ctor
    /// </summary>
    public MarketplaceContext(DbContextOptions options): base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
	    // Конвертер для преобразования DateTimeOffset <-> DateTime (UTC)
	   
	    var dateTimeOffsetConverter = new ValueConverter<DateTimeOffset, DateTime>(
		    v => DateTime.SpecifyKind(v.UtcDateTime, DateTimeKind.Unspecified),
		    v => new DateTimeOffset(DateTime.SpecifyKind(v, DateTimeKind.Utc))
	    );

	    modelBuilder.Entity<Product>()
		    .Property(p => p.CreatedAt)
		    .HasConversion(dateTimeOffsetConverter)
		    .HasColumnType("timestamp without time zone");

	    modelBuilder.Entity<Product>()
		    .Property(p => p.UpdatedAt)
		    .HasConversion(dateTimeOffsetConverter)
		    .HasColumnType("timestamp without time zone");
	    
	    base.OnModelCreating(modelBuilder);
	   
	    /*
	    CREATE TABLE public."Products"
	    (
	        "Id" UUID NOT NULL PRIMARY KEY,
	        "Name" VARCHAR(1000) NOT NULL,
	        "Article" VARCHAR(255) NOT NULL,
	        "Price" INT NOT NULL,
	        "Category" INT NOT NULL,
	        "CreatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	        "UpdatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL
	    )
	    */


	    
    }
}
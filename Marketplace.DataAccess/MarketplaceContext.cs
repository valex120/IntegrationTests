using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        /*
CREATE TABLE public."Products"
(
	"Id" UUID NOT NULL PRIMARY KEY,
	"Name" VARCHAR(1000) NOT NULL,
	"Article" VARCHAR(255) NOT NULL,
	"Price" INT NOT NULL,
	"Category" INT NOT NULL,
	"CreatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	"UpdatedAt" TIMESTAMP WITHOUT TIME ZONE  NOT NULL
)
         */
    }
}
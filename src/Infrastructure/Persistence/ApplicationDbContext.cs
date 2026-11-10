using System.Reflection;
using Domain.Amenities;
using Domain.PlayRooms;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<PlayRoom> PlayRooms { get; init; }
    public DbSet<Amenity> Amenities { get; init; }
    public DbSet<PlayRoomAmenity> PlayRoomAmenities { get; init; }
    public DbSet<PlayRoomImage> PlayRoomImages { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
using Domain.PlayRooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PlayRoomAmenityConfiguration : IEntityTypeConfiguration<PlayRoomAmenity>
{
    public void Configure(EntityTypeBuilder<PlayRoomAmenity> builder)
    {
        builder.HasKey(x => new { x.PlayRoomId, x.AmenityId });
        builder.Property(x => x.PlayRoomId).HasConversion(x => x.Value, x => new PlayRoomId(x));

        builder.HasOne(x => x.PlayRoom)
            .WithMany(x => x.Amenities)
            .HasForeignKey(x => x.PlayRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Amenity)
            .WithMany()
            .HasForeignKey(x => x.AmenityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
using Domain.PlayRooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PlayRoomImageConfiguration : IEntityTypeConfiguration<PlayRoomImage>
{
    public void Configure(EntityTypeBuilder<PlayRoomImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new PlayRoomImageId(x));
        builder.Property(x => x.PlayRoomId).HasConversion(x => x.Value, x => new PlayRoomId(x));

        builder.Property(x => x.OriginalName)
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.HasOne<PlayRoom>()
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.PlayRoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
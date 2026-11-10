using Domain.PlayRooms;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PlayRoomsConfiguration : IEntityTypeConfiguration<PlayRoom>
{
    public void Configure(EntityTypeBuilder<PlayRoom> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new PlayRoomId(x));

        builder.Property(x => x.RoomNumber)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.HasIndex(x => x.RoomNumber)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Description)
            .HasColumnType("varchar(500)")
            .IsRequired();

        builder.Property(x => x.PricePerHour)
            .HasColumnType("numeric(10,2)")
            .IsRequired();

        builder.Property(x => x.Capacity)
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(x => x.HasAnimator)
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(x => x.Theme)
            .HasConversion<string>()
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.OwnsOne(x => x.Attributes, b => b.ToJson());

        builder.Property(x => x.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired(false);
    }
}
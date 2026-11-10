using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExpandDomainModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "attributes",
                table: "play_rooms",
                type: "jsonb",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "play_rooms",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "play_rooms",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "amenities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_amenities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "play_room_images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    original_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    play_room_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_play_room_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_play_room_images_play_rooms_play_room_id",
                        column: x => x.play_room_id,
                        principalTable: "play_rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "play_room_amenities",
                columns: table => new
                {
                    play_room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amenity_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_play_room_amenities", x => new { x.play_room_id, x.amenity_id });
                    table.ForeignKey(
                        name: "fk_play_room_amenities_amenities_amenity_id",
                        column: x => x.amenity_id,
                        principalTable: "amenities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_play_room_amenities_play_rooms_play_room_id",
                        column: x => x.play_room_id,
                        principalTable: "play_rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_play_rooms_name",
                table: "play_rooms",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_amenities_name",
                table: "amenities",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_play_room_amenities_amenity_id",
                table: "play_room_amenities",
                column: "amenity_id");

            migrationBuilder.CreateIndex(
                name: "ix_play_room_images_play_room_id",
                table: "play_room_images",
                column: "play_room_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "play_room_amenities");

            migrationBuilder.DropTable(
                name: "play_room_images");

            migrationBuilder.DropTable(
                name: "amenities");

            migrationBuilder.DropIndex(
                name: "ix_play_rooms_name",
                table: "play_rooms");

            migrationBuilder.DropColumn(
                name: "attributes",
                table: "play_rooms");

            migrationBuilder.DropColumn(
                name: "description",
                table: "play_rooms");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "play_rooms");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    link = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    telehon_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SocialList = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 10, nullable: false),
                    status = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    date_of_creation = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 10, nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    attributes_height = table.Column<double>(type: "double precision", maxLength: 10, nullable: false),
                    attributes = table.Column<double>(type: "double precision", maxLength: 10, nullable: false),
                    breed = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    castration_status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    color = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    nick_name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    owner_telephon_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    status_health = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    vaccination_status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    View = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}

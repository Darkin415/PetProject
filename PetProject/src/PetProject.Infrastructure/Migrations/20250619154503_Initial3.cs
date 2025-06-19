using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    link = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    telehon_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Social = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_volunteers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    height = table.Column<double>(type: "double precision", maxLength: 10, nullable: false),
                    weight = table.Column<double>(type: "double precision", maxLength: 10, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 100, nullable: false),
                    breed = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    castration_status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    color = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 100, nullable: false),
                    nick_name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    owner_telephon_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    status_health = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    vaccination_status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    View = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Photos = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pets_volunteer_id",
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

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "WorkItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedToId",
                table: "WorkItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

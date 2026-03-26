using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditLogStrcutured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "audit_log",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entity_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    entity_id = table.Column<long>(type: "bigint", nullable: false),
                    action = table.Column<string>(type: "varchar(30)", nullable: false),
                    before_state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    after_state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    event_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    audit_version = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_log", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_log");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2Webb.Migrations
{
    /// <inheritdoc />
    public partial class RenameMobileNumberToCellphone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "Customers",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<string>(
                name: "Cellphone",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cellphone",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Customers",
                newName: "MobileNumber");
        }
    }
}

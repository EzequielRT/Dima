using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class atualiza_coluna_CreatedDate_para_CreatedAt_tb_Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Transaction",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Transaction",
                newName: "CreatedDate");
        }
    }
}

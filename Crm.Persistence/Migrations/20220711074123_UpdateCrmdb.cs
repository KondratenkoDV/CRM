using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crm.Persistence.Migrations
{
    public partial class UpdateCrmdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractEmployee_Contracts_ContractsId",
                table: "ContractEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractEmployee_Employees_EmployeesId",
                table: "ContractEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractEmployee",
                table: "ContractEmployee");

            migrationBuilder.RenameTable(
                name: "ContractEmployee",
                newName: "EmployeesAndContracts");

            migrationBuilder.RenameIndex(
                name: "IX_ContractEmployee_EmployeesId",
                table: "EmployeesAndContracts",
                newName: "IX_EmployeesAndContracts_EmployeesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Positions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Contracts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Contracts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PhonNumber",
                table: "Clients",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeesAndContracts",
                table: "EmployeesAndContracts",
                columns: new[] { "ContractsId", "EmployeesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesAndContracts_Contracts_ContractsId",
                table: "EmployeesAndContracts",
                column: "ContractsId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesAndContracts_Employees_EmployeesId",
                table: "EmployeesAndContracts",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesAndContracts_Contracts_ContractsId",
                table: "EmployeesAndContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesAndContracts_Employees_EmployeesId",
                table: "EmployeesAndContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeesAndContracts",
                table: "EmployeesAndContracts");

            migrationBuilder.RenameTable(
                name: "EmployeesAndContracts",
                newName: "ContractEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeesAndContracts_EmployeesId",
                table: "ContractEmployee",
                newName: "IX_ContractEmployee_EmployeesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Positions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PhonNumber",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractEmployee",
                table: "ContractEmployee",
                columns: new[] { "ContractsId", "EmployeesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEmployee_Contracts_ContractsId",
                table: "ContractEmployee",
                column: "ContractsId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEmployee_Employees_EmployeesId",
                table: "ContractEmployee",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

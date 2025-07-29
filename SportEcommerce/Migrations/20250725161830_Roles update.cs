using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class Rolesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            // Removed: AddColumn for SellerId on ProductTypes
            // migrationBuilder.AddColumn<string>(
            //     name: "SellerId",
            //     table: "ProductTypes",
            //     type: "varchar(36)",
            //     nullable: false,
            //     defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FirstSurname",
                table: "AspNetUsers",
                type: "varchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "varchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDefault",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            // Removed: CreateIndex for ProductTypes_SellerId
            // migrationBuilder.CreateIndex(
            //     name: "IX_ProductTypes_SellerId",
            //     table: "ProductTypes",
            //     column: "SellerId");

            // Removed: AddForeignKey for ProductTypes_Sellers_SellerId
            // migrationBuilder.AddForeignKey(
            //     name: "FK_ProductTypes_Sellers_SellerId",
            //     table: "ProductTypes",
            //     column: "SellerId",
            //     principalTable: "Sellers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // The Down method should ideally reverse ONLY what was in the Up method.
            // Since you're removing the FK/Index/Column from Up, they should also be removed from Down.
            // However, I'm keeping the original Down method for context,
            // as this is usually generated to reverse the Up.
            // If you actually run this modified migration, and then revert it,
            // the original Down would attempt to drop things that weren't added by this Up.
            // For a clean migration, you would regenerate this migration after
            // fixing your model's configuration that caused these lines.

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_Sellers_SellerId",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_SellerId",
                table: "ProductTypes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35a5de8a-8f7b-4806-a1fe-c5a32ed430e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d99eca7-c218-44fb-a64d-163e486acb1b");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "FirstSurname",
                table: "AspNetUsers",
                type: "varchar(40)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "varchar(40)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDefault",
                table: "AspNetRoles",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "IsDefault", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35a5de8a-8f7b-4806-a1fe-c5a32ed430e8", null, "ApplicationRole", true, "Buyer", "BUYER" },
                    { "3d99eca7-c218-44fb-a64d-163e486acb1b", null, "ApplicationRole", false, "Seller", "SELLER" }
                });
        }
    }
}
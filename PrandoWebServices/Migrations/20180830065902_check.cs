using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrandoWebServices.Migrations
{
    public partial class check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VehicleOwner");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AutoModel");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AutoMaker");

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    CreatedBy_Id = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy_Id = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    BaseUrl = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_CreatedById",
                table: "Attachment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ModifiedById",
                table: "Attachment",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VehicleOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vehicle",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Owner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AutoModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AutoMaker",
                nullable: false,
                defaultValue: false);
        }
    }
}

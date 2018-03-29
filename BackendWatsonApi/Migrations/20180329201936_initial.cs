using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BackendWatsonApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageUri = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                });

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    PlaceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.PlaceId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPost",
                columns: table => new
                {
                    UserPostId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    PlaceId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPost", x => x.UserPostId);
                    table.ForeignKey(
                        name: "FK_UserPost_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPost_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPost_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatsonClassification",
                columns: table => new
                {
                    ClassificationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Class = table.Column<string>(nullable: false),
                    ClassifierId = table.Column<string>(nullable: false),
                    ClassifierName = table.Column<string>(nullable: false),
                    ConfidenceScore = table.Column<string>(nullable: false),
                    TypeHierarchy = table.Column<string>(nullable: true),
                    UserPostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatsonClassification", x => x.ClassificationId);
                    table.ForeignKey(
                        name: "FK_WatsonClassification_UserPost_UserPostId",
                        column: x => x.UserPostId,
                        principalTable: "UserPost",
                        principalColumn: "UserPostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPost_ImageId",
                table: "UserPost",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPost_PlaceId",
                table: "UserPost",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPost_UserId",
                table: "UserPost",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WatsonClassification_UserPostId",
                table: "WatsonClassification",
                column: "UserPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatsonClassification");

            migrationBuilder.DropTable(
                name: "UserPost");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

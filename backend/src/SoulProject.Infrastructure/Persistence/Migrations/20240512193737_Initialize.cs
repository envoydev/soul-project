using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoulProject.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    RefreshToken = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Traits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Side = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Traits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrustCircles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    PositionIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrustCircles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrustCircles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Souls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TrustCircleId = table.Column<Guid>(type: "TEXT", nullable: true),
                    OccupationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AvatarPath = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Nickname = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IncomeAmount = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    IncomeCurrency = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    Added = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Meet = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Souls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Souls_TrustCircles_TrustCircleId",
                        column: x => x.TrustCircleId,
                        principalTable: "TrustCircles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Souls_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoulSkills",
                columns: table => new
                {
                    SoulId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SkillId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoulSkills", x => new { x.SoulId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_SoulSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SoulSkills_Souls_SoulId",
                        column: x => x.SoulId,
                        principalTable: "Souls",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoulTraits",
                columns: table => new
                {
                    SoulId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TraitId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoulTraits", x => new { x.SoulId, x.TraitId });
                    table.ForeignKey(
                        name: "FK_SoulTraits_Souls_SoulId",
                        column: x => x.SoulId,
                        principalTable: "Souls",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SoulTraits_Traits_TraitId",
                        column: x => x.TraitId,
                        principalTable: "Traits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_UserId",
                table: "Skills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Souls_TrustCircleId",
                table: "Souls",
                column: "TrustCircleId");

            migrationBuilder.CreateIndex(
                name: "IX_Souls_UserId",
                table: "Souls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SoulSkills_SkillId",
                table: "SoulSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SoulTraits_TraitId",
                table: "SoulTraits",
                column: "TraitId");

            migrationBuilder.CreateIndex(
                name: "IX_Traits_Name_Side",
                table: "Traits",
                columns: new[] { "Name", "Side" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Traits_UserId",
                table: "Traits",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustCircles_Name",
                table: "TrustCircles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrustCircles_UserId",
                table: "TrustCircles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoulSkills");

            migrationBuilder.DropTable(
                name: "SoulTraits");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Souls");

            migrationBuilder.DropTable(
                name: "Traits");

            migrationBuilder.DropTable(
                name: "TrustCircles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

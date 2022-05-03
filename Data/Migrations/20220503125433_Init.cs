using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Checkpoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkpoint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessLevelCheckpoint",
                columns: table => new
                {
                    AccessLevelsId = table.Column<int>(type: "int", nullable: false),
                    CheckpointsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLevelCheckpoint", x => new { x.AccessLevelsId, x.CheckpointsId });
                    table.ForeignKey(
                        name: "FK_AccessLevelCheckpoint_AccessLevel_AccessLevelsId",
                        column: x => x.AccessLevelsId,
                        principalTable: "AccessLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessLevelCheckpoint_Checkpoint_CheckpointsId",
                        column: x => x.CheckpointsId,
                        principalTable: "Checkpoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckpointRoom",
                columns: table => new
                {
                    CheckpointsId = table.Column<int>(type: "int", nullable: false),
                    RoomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckpointRoom", x => new { x.CheckpointsId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_CheckpointRoom_Checkpoint_CheckpointsId",
                        column: x => x.CheckpointsId,
                        principalTable: "Checkpoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckpointRoom_Room_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(35)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CurrentRoomId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Room_CurrentRoomId",
                        column: x => x.CurrentRoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessLevelUser",
                columns: table => new
                {
                    AccessLevelsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLevelUser", x => new { x.AccessLevelsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AccessLevelUser_AccessLevel_AccessLevelsId",
                        column: x => x.AccessLevelsId,
                        principalTable: "AccessLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessLevelUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthUserRoomTimeSpent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    TotalTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthUserRoomTimeSpent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthUserRoomTimeSpent_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonthUserRoomTimeSpent_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PassageDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CheckpointId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassageDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassageDate_Checkpoint_CheckpointId",
                        column: x => x.CheckpointId,
                        principalTable: "Checkpoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassageDate_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomTimeSpent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    EnterPassageDateId = table.Column<int>(type: "int", nullable: false),
                    ExitPassageDateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTimeSpent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTimeSpent_PassageDate_EnterPassageDateId",
                        column: x => x.EnterPassageDateId,
                        principalTable: "PassageDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomTimeSpent_PassageDate_ExitPassageDateId",
                        column: x => x.ExitPassageDateId,
                        principalTable: "PassageDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomTimeSpent_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Worker" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessLevelCheckpoint_CheckpointsId",
                table: "AccessLevelCheckpoint",
                column: "CheckpointsId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessLevelUser_UsersId",
                table: "AccessLevelUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckpointRoom_RoomsId",
                table: "CheckpointRoom",
                column: "RoomsId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthUserRoomTimeSpent_RoomId",
                table: "MonthUserRoomTimeSpent",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthUserRoomTimeSpent_UserId",
                table: "MonthUserRoomTimeSpent",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PassageDate_CheckpointId",
                table: "PassageDate",
                column: "CheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_PassageDate_UserId",
                table: "PassageDate",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTimeSpent_EnterPassageDateId",
                table: "RoomTimeSpent",
                column: "EnterPassageDateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomTimeSpent_ExitPassageDateId",
                table: "RoomTimeSpent",
                column: "ExitPassageDateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomTimeSpent_RoomId",
                table: "RoomTimeSpent",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CurrentRoomId",
                table: "User",
                column: "CurrentRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PositionId",
                table: "User",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessLevelCheckpoint");

            migrationBuilder.DropTable(
                name: "AccessLevelUser");

            migrationBuilder.DropTable(
                name: "CheckpointRoom");

            migrationBuilder.DropTable(
                name: "MonthUserRoomTimeSpent");

            migrationBuilder.DropTable(
                name: "RoomTimeSpent");

            migrationBuilder.DropTable(
                name: "AccessLevel");

            migrationBuilder.DropTable(
                name: "PassageDate");

            migrationBuilder.DropTable(
                name: "Checkpoint");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}

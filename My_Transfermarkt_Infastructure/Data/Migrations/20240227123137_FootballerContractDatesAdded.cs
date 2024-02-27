using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Transfermarkt.Data.Migrations
{
    public partial class FootballerContractDatesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentsFootballers_Agent_AgentId",
                table: "AgentsFootballers");

            migrationBuilder.DropForeignKey(
                name: "FK_AgentsFootballers_Footballer_FootballerId",
                table: "AgentsFootballers");

            migrationBuilder.DropForeignKey(
                name: "FK_Footballer_Agent_AgentId",
                table: "Footballer");

            migrationBuilder.DropForeignKey(
                name: "FK_Footballer_Country_CountryId",
                table: "Footballer");

            migrationBuilder.DropForeignKey(
                name: "FK_Footballer_Team_TeamId",
                table: "Footballer");

            migrationBuilder.DropForeignKey(
                name: "FK_Stadium_Country_CountryId",
                table: "Stadium");

            migrationBuilder.DropForeignKey(
                name: "FK_StadiumsTeams_Stadium_StadiumId",
                table: "StadiumsTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_StadiumsTeams_Team_TeamId",
                table: "StadiumsTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Country_CountryId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Footballer_FootballerId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Stadium_StadiumId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamsFootballers_Footballer_FootballerId",
                table: "TeamsFootballers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamsFootballers_Team_TeamId",
                table: "TeamsFootballers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team",
                table: "Team");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stadium",
                table: "Stadium");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Footballer",
                table: "Footballer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.RenameTable(
                name: "Team",
                newName: "Teams");

            migrationBuilder.RenameTable(
                name: "Stadium",
                newName: "Stadiums");

            migrationBuilder.RenameTable(
                name: "Footballer",
                newName: "Footballers");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "Agents");

            migrationBuilder.RenameIndex(
                name: "IX_Team_StadiumId",
                table: "Teams",
                newName: "IX_Teams_StadiumId");

            migrationBuilder.RenameIndex(
                name: "IX_Team_FootballerId",
                table: "Teams",
                newName: "IX_Teams_FootballerId");

            migrationBuilder.RenameIndex(
                name: "IX_Team_CountryId",
                table: "Teams",
                newName: "IX_Teams_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Stadium_CountryId",
                table: "Stadiums",
                newName: "IX_Stadiums_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Footballer_TeamId",
                table: "Footballers",
                newName: "IX_Footballers_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Footballer_CountryId",
                table: "Footballers",
                newName: "IX_Footballers_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Footballer_AgentId",
                table: "Footballers",
                newName: "IX_Footballers_AgentId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateContract",
                table: "Footballers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateContract",
                table: "Footballers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stadiums",
                table: "Stadiums",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Footballers",
                table: "Footballers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentsFootballers_Agents_AgentId",
                table: "AgentsFootballers",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgentsFootballers_Footballers_FootballerId",
                table: "AgentsFootballers",
                column: "FootballerId",
                principalTable: "Footballers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Footballers_Agents_AgentId",
                table: "Footballers",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Footballers_Countries_CountryId",
                table: "Footballers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Footballers_Teams_TeamId",
                table: "Footballers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stadiums_Countries_CountryId",
                table: "Stadiums",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumsTeams_Stadiums_StadiumId",
                table: "StadiumsTeams",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumsTeams_Teams_TeamId",
                table: "StadiumsTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Countries_CountryId",
                table: "Teams",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Footballers_FootballerId",
                table: "Teams",
                column: "FootballerId",
                principalTable: "Footballers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Stadiums_StadiumId",
                table: "Teams",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsFootballers_Footballers_FootballerId",
                table: "TeamsFootballers",
                column: "FootballerId",
                principalTable: "Footballers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsFootballers_Teams_TeamId",
                table: "TeamsFootballers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentsFootballers_Agents_AgentId",
                table: "AgentsFootballers");

            migrationBuilder.DropForeignKey(
                name: "FK_AgentsFootballers_Footballers_FootballerId",
                table: "AgentsFootballers");

            migrationBuilder.DropForeignKey(
                name: "FK_Footballers_Agents_AgentId",
                table: "Footballers");

            migrationBuilder.DropForeignKey(
                name: "FK_Footballers_Countries_CountryId",
                table: "Footballers");

            migrationBuilder.DropForeignKey(
                name: "FK_Footballers_Teams_TeamId",
                table: "Footballers");

            migrationBuilder.DropForeignKey(
                name: "FK_Stadiums_Countries_CountryId",
                table: "Stadiums");

            migrationBuilder.DropForeignKey(
                name: "FK_StadiumsTeams_Stadiums_StadiumId",
                table: "StadiumsTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_StadiumsTeams_Teams_TeamId",
                table: "StadiumsTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Countries_CountryId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Footballers_FootballerId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Stadiums_StadiumId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamsFootballers_Footballers_FootballerId",
                table: "TeamsFootballers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamsFootballers_Teams_TeamId",
                table: "TeamsFootballers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stadiums",
                table: "Stadiums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Footballers",
                table: "Footballers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "EndDateContract",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "StartDateContract",
                table: "Footballers");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Team");

            migrationBuilder.RenameTable(
                name: "Stadiums",
                newName: "Stadium");

            migrationBuilder.RenameTable(
                name: "Footballers",
                newName: "Footballer");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Agents",
                newName: "Agent");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_StadiumId",
                table: "Team",
                newName: "IX_Team_StadiumId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_FootballerId",
                table: "Team",
                newName: "IX_Team_FootballerId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_CountryId",
                table: "Team",
                newName: "IX_Team_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Stadiums_CountryId",
                table: "Stadium",
                newName: "IX_Stadium_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Footballers_TeamId",
                table: "Footballer",
                newName: "IX_Footballer_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Footballers_CountryId",
                table: "Footballer",
                newName: "IX_Footballer_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Footballers_AgentId",
                table: "Footballer",
                newName: "IX_Footballer_AgentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team",
                table: "Team",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stadium",
                table: "Stadium",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Footballer",
                table: "Footballer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentsFootballers_Agent_AgentId",
                table: "AgentsFootballers",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgentsFootballers_Footballer_FootballerId",
                table: "AgentsFootballers",
                column: "FootballerId",
                principalTable: "Footballer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Footballer_Agent_AgentId",
                table: "Footballer",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Footballer_Country_CountryId",
                table: "Footballer",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Footballer_Team_TeamId",
                table: "Footballer",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stadium_Country_CountryId",
                table: "Stadium",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumsTeams_Stadium_StadiumId",
                table: "StadiumsTeams",
                column: "StadiumId",
                principalTable: "Stadium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumsTeams_Team_TeamId",
                table: "StadiumsTeams",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Country_CountryId",
                table: "Team",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Footballer_FootballerId",
                table: "Team",
                column: "FootballerId",
                principalTable: "Footballer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Stadium_StadiumId",
                table: "Team",
                column: "StadiumId",
                principalTable: "Stadium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsFootballers_Footballer_FootballerId",
                table: "TeamsFootballers",
                column: "FootballerId",
                principalTable: "Footballer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsFootballers_Team_TeamId",
                table: "TeamsFootballers",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

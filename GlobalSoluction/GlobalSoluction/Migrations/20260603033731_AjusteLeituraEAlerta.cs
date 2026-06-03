using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalSoluction.Migrations
{
    /// <inheritdoc />
    public partial class AjusteLeituraEAlerta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertasEstufa_Sensores_SensorId",
                table: "AlertasEstufa");

            migrationBuilder.DropForeignKey(
                name: "FK_LeiturasSensor_Sensores_SensorId",
                table: "LeiturasSensor");

            migrationBuilder.DropIndex(
                name: "IX_LeiturasSensor_SensorId",
                table: "LeiturasSensor");

            migrationBuilder.DropIndex(
                name: "IX_AlertasEstufa_SensorId",
                table: "AlertasEstufa");

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "AlertasEstufa");

            migrationBuilder.RenameColumn(
                name: "SensorId",
                table: "LeiturasSensor",
                newName: "TipoSensor");

            migrationBuilder.AddColumn<int>(
                name: "EstufaConfigId",
                table: "LeiturasSensor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoSensor",
                table: "AlertasEstufa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LeiturasSensor_EstufaConfigId",
                table: "LeiturasSensor",
                column: "EstufaConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeiturasSensor_EstufasConfig_EstufaConfigId",
                table: "LeiturasSensor",
                column: "EstufaConfigId",
                principalTable: "EstufasConfig",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeiturasSensor_EstufasConfig_EstufaConfigId",
                table: "LeiturasSensor");

            migrationBuilder.DropIndex(
                name: "IX_LeiturasSensor_EstufaConfigId",
                table: "LeiturasSensor");

            migrationBuilder.DropColumn(
                name: "EstufaConfigId",
                table: "LeiturasSensor");

            migrationBuilder.DropColumn(
                name: "TipoSensor",
                table: "AlertasEstufa");

            migrationBuilder.RenameColumn(
                name: "TipoSensor",
                table: "LeiturasSensor",
                newName: "SensorId");

            migrationBuilder.AddColumn<int>(
                name: "SensorId",
                table: "AlertasEstufa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeiturasSensor_SensorId",
                table: "LeiturasSensor",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasEstufa_SensorId",
                table: "AlertasEstufa",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertasEstufa_Sensores_SensorId",
                table: "AlertasEstufa",
                column: "SensorId",
                principalTable: "Sensores",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LeiturasSensor_Sensores_SensorId",
                table: "LeiturasSensor",
                column: "SensorId",
                principalTable: "Sensores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

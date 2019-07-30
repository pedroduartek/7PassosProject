using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SetepassosPRJ.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModeloDeHiscores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nickname = table.Column<string>(nullable: true),
                    MoedasDeOuro = table.Column<double>(nullable: false),
                    ResultadoFinal = table.Column<int>(nullable: false),
                    ChaveEncontrada = table.Column<bool>(nullable: false),
                    InimigosDerrotados = table.Column<int>(nullable: false),
                    FugasDoInimigo = table.Column<int>(nullable: false),
                    InvestigacaoDaArea = table.Column<int>(nullable: false),
                    NumeroDeItensEncontrados = table.Column<int>(nullable: false),
                    PocoesUsadas = table.Column<int>(nullable: false),
                    PocoesTotais = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloDeHiscores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModeloDeHiscores");
        }
    }
}

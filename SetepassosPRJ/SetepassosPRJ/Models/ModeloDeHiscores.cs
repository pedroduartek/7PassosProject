using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class ModeloDeHiscores : IComparable
    {
        public int Id { get; set; }

        public string Nickname { get; set; }

        public double MoedasDeOuro { get; set; }

        public Result ResultadoFinal { get; set; }

        public bool ChaveEncontrada { get; set; }

        public int InimigosDerrotados { get; set; }

        public int FugasDoInimigo { get; set; }

        public int InvestigacaoDaArea { get; set; }

        public int NumeroDeItensEncontrados { get; set; }

        public int PocoesUsadas { get; set; }

        public int PocoesTotais { get; set; }

        public ModeloDeHiscores()
        {

        }

        public ModeloDeHiscores(string nickName, int moedasDeOuro, Result resultadoFinal, bool chaveEncontrada, int inimigosDerrotados, int fugasDoInimigo, int investigacaoDaArea, int numeroDeItensEncontrados, int pocoesUsadas, int pocoesTotais)
        {
            Nickname = nickName;
            MoedasDeOuro = moedasDeOuro;
            ResultadoFinal = resultadoFinal;
            ChaveEncontrada = chaveEncontrada;
            InimigosDerrotados = inimigosDerrotados;
            FugasDoInimigo = fugasDoInimigo;
            InvestigacaoDaArea = investigacaoDaArea;
            NumeroDeItensEncontrados = numeroDeItensEncontrados;
            PocoesUsadas = pocoesUsadas;
            PocoesTotais = pocoesTotais;

        }

        public int CompareTo(object obj)
        {
            ModeloDeHiscores hiscores2 = (ModeloDeHiscores)obj;

            if (hiscores2.MoedasDeOuro > MoedasDeOuro)
                return 1;
            if (hiscores2.MoedasDeOuro == MoedasDeOuro)
                return 0;
            return -1;
        }
    }
}

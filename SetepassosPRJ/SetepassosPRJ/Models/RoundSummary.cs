using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class RoundSummary
    {
        public int NumeroDaRonda { get; set; }
        public Playeraction DecisaoTomada { get; set; }
        public int PosicaoNoFinalDaRonda { get; set; }
        public int TotalAcumuladoDeInimigosVencidos { get; set; }
        public int TotalAcumuladoDefugasAoCombate { get; set; }
        public int TotalAcumuladoDeItensEncontrados { get; set; }
        public bool PosseDaChave { get; set; }
        public bool ChaveEncontrada { get; set; }
        public double PontosDeVida { get; set; }
        public double PontosDeAtaque { get; set; }
        public double PontosDeSorte { get; set; }
        public int PocoesDeVida { get; set; }
        public Result Resultado { get; set; }


        public RoundSummary()
        {
        }

        public void AtualizarDados(Playeraction dt,int Pocisaofim,int iVencidos,int fugas,int itensencontrados, bool chave,double vida,double ataque,double sorte,int pocoes, Result resultado, int ronda )
        {
            DecisaoTomada = dt;
            PosicaoNoFinalDaRonda = Pocisaofim;
            TotalAcumuladoDeInimigosVencidos = iVencidos;
            TotalAcumuladoDefugasAoCombate = fugas;
            TotalAcumuladoDeItensEncontrados = itensencontrados;
            PosseDaChave = chave;
            PontosDeVida = vida;
            PontosDeAtaque = ataque;
            PontosDeSorte = sorte;
            PocoesDeVida = pocoes;
            NumeroDaRonda = 1;
            Resultado = resultado;
            NumeroDaRonda = ronda;
        }

    }

}

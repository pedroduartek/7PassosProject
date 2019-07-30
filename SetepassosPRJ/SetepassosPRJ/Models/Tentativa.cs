using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SetepassosPRJ.Models;

namespace SetepassosPRJ.Models
{
    public class Tentativa : IComparable
    {
        [Required(ErrorMessage = "Por favor escolha uma classe!")]
        public string Classe { get; set; }

        [Required(ErrorMessage = "Por favor introduza um nome de jogo!")]
        public string Nickname { get; set; }

        public int Id { get; set; }

        public string NomeDaClasse { get; set; }

        public string SkinDaClasse { get; set; }

        public double PontosDeVida { get; set; }

        public double PontosDeForca { get; set; }

        public double PontosDeSorte { get; set; }

        public int MoedasDeOuro { get; set; }

        public int PocoesDeVida { get; set; }

        public int Posicao { get; set; }

        public string Zona { get; set; }

        public Playeraction Acao { get; set; }

        public Result Resultado { get; set; }

        public bool JogoFinalizado { get; set; }

        public string Minimapa { get; set; }

        public bool ChaveNoBolso { get; set; }

        public bool ChaveNoChao { get; set; }

        public bool Inimigo { get; set; }

        public double PontosDeVidaInimigo { get; set; }

        public double PontosDeSorteInimigo { get; set; }

        public double PontosDeAtaqueInimigo { get; set; }

        public double DanoSofrido { get; set; }

        public int ItemEfeitoVida { get; set; }

        public int ItemEfeitoForca { get; set; }

        public int ItemEfeitoSorte { get; set; }

        public int MoedasDeOuroEncontradas { get; set; }

        public int ContadorMovimentos { get; set; }

        public int ContadorAtaques { get; set; }

        public int ContadorPesquisas { get; set; }

        public int ContadorItensEncontrados { get; set; }

        public bool PocaoDeVidaEncontrada { get; set; }

        public bool ItemEncontrado { get; set; }

        public int InimigosDerrotados { get; set; }

        public int InimigosFintados { get; set; }

        public int PocoesUsadas { get; set; }

        public int PocoesTotais { get; set; }

        public bool Recuou { get; set; }

        public int Ronda { get; set; }

        public bool[] AreaExaminada { get; set; }

        public bool HeroiCansado { get; set; }

        public int VidaMaxima { get; set; }

        public double PercetagemAreaInvestigada { get; set; }

        public double NumeroAreasExaminadas { get; set; }

        private List<RoundSummary> rs = new List<RoundSummary>(); 

        public List<RoundSummary> Rs
        {
            get
            {
                return rs;
            }
        }

        public void AdicionarRoundSummary(RoundSummary rs)
        {
            Rs.Add(rs);
        }


        public void IniciarTentativa()
        {
            AreaExaminada = new bool[8];
            MoedasDeOuro = 0;
            PocoesDeVida = 1;
            Posicao = 1;
            PocoesTotais = 1;
            AtualizarPosicao();
            if (Classe == "B")
                VidaMaxima = 4;
            else
                VidaMaxima = 3;




            if (Classe == "S")
            {
                PontosDeVida = VidaMaxima;
                PontosDeForca = 3;
                PontosDeSorte = 3;
                NomeDaClasse = "Pedro";
                SkinDaClasse = "/images/skinpedro.png";
            }
            else if (Classe == "W")
            {
                PontosDeVida = VidaMaxima;
                PontosDeForca = 2;
                PontosDeSorte = 4;
                NomeDaClasse = "Tiago";
                SkinDaClasse = "/images/skintiago.png";
            }
            else // "B"
            {
                PontosDeVida = 4;
                PontosDeForca = 3;
                PontosDeSorte = 2;
                NomeDaClasse = "Pereira";
                SkinDaClasse = "/images/skinpereira.png";
            }
        }

        public int CompareTo(object obj)
        {
            Tentativa tentativa2 = (Tentativa)obj;

            if (tentativa2.MoedasDeOuro > MoedasDeOuro)
                return 1;
            if (tentativa2.MoedasDeOuro == MoedasDeOuro)
                return 0;
            return -1;
        }

        public void ExecutarAcao()
        {
            if (PontosDeVida <= 0)
            {
                FinalizarTentativa();
            }
            if (!JogoFinalizado)
            {
                if (Acao == Playeraction.GoForward)
                {
                    if (Posicao <= 7)
                        Posicao++;

                    if (Posicao == 8 && ChaveNoBolso)
                    {
                        FinalizarTentativa();
                        ContadorMovimentos--; // O Movimento de entrar na porta não conta
                    }
                    ContadorMovimentos++;
                }


                else if (Acao == Playeraction.GoBack)
                {
                    if (Posicao > 1)
                        Posicao--;

                    Recuou = true;
                    ContadorMovimentos++;
                }

                else if (Acao == Playeraction.Attack)
                {
                    ContadorAtaques++;

                    if (PontosDeVidaInimigo <= 0)
                        InimigosDerrotados++;
                }

                else if (Acao == Playeraction.SearchArea)
                {
                    ContadorPesquisas++;
                    ExaminarArea(Posicao - 1);
                }

                else if (Acao == Playeraction.DrinkPotion)
                {
                    if (PocoesDeVida >= 1)
                    {
                        if (Classe == "B")
                        {
                            if (PontosDeVida < 4)
                            {
                                PontosDeVida = 4;
                                PocoesDeVida--;
                            }
                        }
                        else
                        {
                            if (PontosDeVida < 3) // Imagina que o Heroi tem mais que 3 corações que ganhou durante o jogo, bebe uma poção e perde vida
                            {
                                PontosDeVida = 3;
                                PocoesDeVida--;
                            }
                        }
                    }
                    PocoesUsadas++;
                }

                else if (Acao == Playeraction.Flee)
                {
                    PontosDeVida -= DanoSofrido;
                    if (PontosDeVida <= 0)
                    {
                        FinalizarTentativa();
                    }
                    else
                    {
                        InimigosFintados++;

                        if (Posicao <= 7)
                            Posicao++;

                        if (Posicao == 8 && ChaveNoBolso)
                        {
                            FinalizarTentativa();
                        }
                        else if (Posicao == 7 && !ChaveNoBolso)
                            Posicao--;

                        ContadorMovimentos++;
                    }
                }
                AtualizarPosicao();
                AtualizarStats();
            }
        }


        public void AtualizarPosicao()
        {
            Zona = "zona" + Posicao + ".png";
            Minimapa = "miniMapaP" + Posicao + ".png";
        }

        public void AtualizarDados(GameState gs)
        {
            Acao = gs.Action;
            ChaveNoChao = gs.FoundKey;
            DanoSofrido = gs.EnemyDamageSuffered;
            Id = gs.GameId;
            Inimigo = gs.FoundEnemy;
            ItemEfeitoForca = gs.ItemAttackEffect;
            ItemEfeitoSorte = gs.ItemLuckEffect;
            ItemEfeitoVida = gs.ItemHealthEffect;
            ItemEncontrado = gs.FoundItem;
            MoedasDeOuroEncontradas = gs.GoldFound;
            PontosDeSorteInimigo = gs.EnemyLuckPoints;
            PontosDeAtaqueInimigo = gs.EnemyAttackPoints;
            PontosDeVidaInimigo = gs.EnemyHealthPoints;
            PocaoDeVidaEncontrada = gs.FoundPotion;
            Ronda = gs.RoundNumber;
            Resultado = gs.Result;
        }


        public void VerificarCansaco()
        {
            if (ContadorAtaques > 7 || ContadorMovimentos > 7 || ContadorPesquisas > 7)
                HeroiCansado = true;
            else
                HeroiCansado = false;
        }

        public void CalcularBonus()
        {
            if (ChaveNoBolso)
                AddBonus(1000);
            if (Resultado == Result.SuccessVictory)
            {
                AddBonus(3000);
                if (PontosDeVida < 0.5)
                    AddBonus(999);
                if (ContadorAtaques == 0)
                    AddBonus(800);
                if (!Recuou)
                    AddBonus(400);
            }

            if (PocoesDeVida > 0)
                AddBonus(PocoesDeVida * 750);

            if (InimigosDerrotados > 0)
                AddBonus(InimigosDerrotados * 300);

            AddBonus(ContadorItensEncontrados * 100);
        }

        public void AddBonus(int bonus)
        {
            MoedasDeOuro += bonus;
        }

        public void ExaminarArea(int area)
        {
            AreaExaminada[area] = true;
        }

        public void AtualizarStats()
        {
            if (ItemEncontrado)
            {
                ContadorItensEncontrados++;
                if (ItemEfeitoForca != 0)
                {
                    PontosDeForca += (ItemEfeitoForca);
                    if (PontosDeForca < 0)
                        PontosDeForca = 0;
                    if (PontosDeForca > 5)
                        PontosDeForca = 5;

                }
                if (ItemEfeitoSorte != 0)
                {
                    PontosDeSorte += (ItemEfeitoSorte);
                    if (PontosDeSorte < 0)
                        PontosDeSorte = 0;
                    if (PontosDeSorte > 5)
                        PontosDeSorte = 5;
                }
                if (ItemEfeitoVida > 0)
                {
                    PontosDeVida += ItemEfeitoVida;
                    if (PontosDeVida > 5)
                        PontosDeVida = 5;
                }
                if (ItemEfeitoVida < 0)
                    DanoSofrido -= ItemEfeitoVida;
            }

            if (MoedasDeOuroEncontradas > 0)
                MoedasDeOuro += MoedasDeOuroEncontradas;

            if (PocaoDeVidaEncontrada)
                if (PocoesDeVida < 3)
                {
                    PocoesDeVida++;
                    PocoesTotais++;
                }

            if (ChaveNoChao)
                ChaveNoBolso = true;

            VerificarCansaco();

            if (HeroiCansado)
                DanoSofrido += 0.5;

            PocoesTotais = PocoesUsadas + PocoesDeVida;
            PontosDeVida -= DanoSofrido;

            PontosDeVidaInimigo = Math.Round(PontosDeVidaInimigo, 2); // 2 = 2 casas décimais, ou seja, se for 2.22222 = 2.2
            PontosDeVida = Math.Round(PontosDeVida, 2);
        }

        public void FinalizarTentativa()
        {
            CalcularPercentagemAreaExaminada();
            CalcularBonus();
            JogoFinalizado = true;
        }

        public void FinalizarRonda(RoundSummary rs)
        {
            AdicionarRoundSummary(rs);
        }

        public void ExecutarAlgoritmo()
        {
            if (Inimigo)
            {
                if (MonstroMuitoForte()) // Não fugir do monstro da ultima casa se não tiver a chave, porque há uma grande chance de ele ter a chave
                {
                    if (PocoesNoBolso() && ((HeroiCansado && PontosDeVida <= 2.3) || (!HeroiCansado && PontosDeVida <= 1.8))) // aqui contabilzia o dano que o monstro muito forte pode dar + o possivel cansaço
                    {
                        Acao = Playeraction.DrinkPotion;
                    }
                    else if (ChaveNoBolso)
                    {
                        if (PocoesNoBolso() && ((HeroiCansado && PontosDeVida <= 3.3) || (!HeroiCansado && PontosDeVida <= 2.8))) // Não sei qual é a lógica, mas por algum motivo levas muito mais dano quando foges
                        {
                            Acao = Playeraction.DrinkPotion;
                        }
                        else
                        {
                            Acao = Playeraction.Flee;
                        }
                    }
                    else
                    {
                        Acao = Playeraction.Attack;
                    }
                }
                else // Monstro acessivel ou está na ultima casa sem chave
                {
                    if (PocoesNoBolso() && ((HeroiCansado && PontosDeVida <= 1.5) || (PontosDeVida <= 1 && !HeroiCansado))) // precisa de poção
                    {
                        Acao = Playeraction.DrinkPotion;
                    }
                    else // não precisa de poção
                    {
                        Acao = Playeraction.Attack;
                    }
                }
            }
            else // Sem Inimigo
            {
                if (HeroiCansado && PontosDeVida <= 0.5 && PocoesNoBolso()) // Se tiver cansado e não beber a poção antes de fazer a ação, vai morrer a toa
                {
                    Acao = Playeraction.DrinkPotion;
                }
                else
                {
                    if (AreaExaminada[Posicao - 1])
                    {
                        if (Posicao != 7 || (Posicao == 7 & ChaveNoBolso))
                        {
                            Acao = Playeraction.GoForward;
                        }
                        else
                        {
                            Acao = Playeraction.GoBack; // Está na ultima casa, já procurou a chave, já matou os montros, então a chave está algures protegida por um monstro muito forte o qualele não vai conseguir matar, portanto o heroi dá em louco e começa a andar para a frente e para trás até morrer
                        }
                    }
                    else // Area por examinar
                    {
                        if (Posicao == 7 && ChaveNoBolso && PontosDeVida < 2 && !PocoesNoBolso()) // Estás na ultima posicao, já tens a chave, não tens poções, estás a 2 de vida, se fores investigar a area podes encontrar um monstro e morrer estando tão perto da porte, é mais viavel não arriscar neste caso
                        {
                            Acao = Playeraction.GoForward;
                        }
                        else
                        {
                            Acao = Playeraction.SearchArea;
                        }
                    }
                }
            }
        }

        public bool MonstroMuitoForte() //sistema de pontos para ver se o monstro é muito forte
        {
            double pontos = new double();
            double bonus = new double();

            if (ChaveNoBolso)
                bonus = 1.25;
            else
                bonus = 1;

            pontos += ((PontosDeVida - PontosDeVidaInimigo) + (PontosDeForca - PontosDeAtaqueInimigo) + (PontosDeSorte - PontosDeSorteInimigo)) * bonus;

            if (pontos >= -1)
                return false;
            else
                return true;
        }

        public bool PocoesNoBolso()
        {
            if (PocoesDeVida > 0)
                return true;
            else
                return false;
        }

        public bool VidaCheia()
        {
            if ((PontosDeVida >= 4 && !HeroiCansado) || (PontosDeVida >= 3.5 && HeroiCansado))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CalcularPercentagemAreaExaminada()
        {
            for (int i = 0; i < 7; i++)
            {
                if (AreaExaminada[i])
                {
                    NumeroAreasExaminadas++;
                }
            }
            PercetagemAreaInvestigada = (NumeroAreasExaminadas / 7) * 100;
            PercetagemAreaInvestigada = Convert.ToInt32(PercetagemAreaInvestigada);
        }

        public bool UltimaCasaSemChave()
        {
            if (Posicao == 7 && !ChaveNoBolso)
                return true;
            else
                return false;
        }
    }
}

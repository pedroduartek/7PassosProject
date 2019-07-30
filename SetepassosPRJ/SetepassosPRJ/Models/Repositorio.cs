using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class Repositorio
    {

        private static TeamMember pereira = new TeamMember("comerestus", "João Pereira", 170323032);

        private static TeamMember pedro = new TeamMember("comerestus", "Pedro Duarte", 170323017);

        private static List<TeamMember> teamMembers = new List<TeamMember>() { pereira, pedro };

        private static List<Tentativa> tentativas = new List<Tentativa>();

        private static List<ModeloDeHiscores> modeloDeHiscores = new List<ModeloDeHiscores>();

        public static List<TeamMember> TeamMembers
        {
            get
            {
                return teamMembers;
            }
        }

        public static List<ModeloDeHiscores> Hiscores
        {
            get
            {
                ComerestusDbContext context = new ComerestusDbContext();
                List<ModeloDeHiscores> hiscores = context.ModeloDeHiscores.ToList();
                return hiscores;
            }
        }

        public static void AddHiscores(ModeloDeHiscores hiscores)
        {
            ComerestusDbContext context = new ComerestusDbContext();
            context.ModeloDeHiscores.Add(hiscores);
            context.SaveChanges();
        }

        public static List<ModeloDeHiscores> filtroDoTop10()
        {
            List<ModeloDeHiscores> todosOsHiscores = Repositorio.Hiscores;
            todosOsHiscores.Sort();
            List<ModeloDeHiscores> top10DosHiscores = new List<ModeloDeHiscores>();

            foreach (ModeloDeHiscores hs in todosOsHiscores)
            {
                top10DosHiscores.Add(hs);
                if (top10DosHiscores.Count == 10)
                {
                    break;
                }
            }
            return top10DosHiscores;
        }


        public static List<ModeloDeHiscores> Listahiscores(ApiHiscores apiHiscores)
        {
            List<ModeloDeHiscores> hiscores = new List<ModeloDeHiscores>();
            int NumeroDeHiscores = 0;
            foreach (ModeloDeHiscores h in Hiscores)
            {
                if (apiHiscores.NickName == "")
                {
                    hiscores.Add(h);
                    NumeroDeHiscores++;
                }
                else
                {
                    if (h.Nickname == apiHiscores.NickName)
                    {
                        hiscores.Add(h);
                        NumeroDeHiscores++;
                    }
                }

                if (NumeroDeHiscores == apiHiscores.NumeroDeHiscoresParaApresentar)
                {
                    return hiscores;
                }
            }
            return hiscores;
        }

        public static List<Tentativa> Tentativas
        {
            get
            {
                return tentativas;
            }
        }

        public static void AddTentativa(Tentativa novaTentativa)
        {
            Tentativas.Add(novaTentativa);
        }

        public static Tentativa ObterTentativa(int id)
        {
            foreach (Tentativa tentativa in Tentativas)
            {
                if (tentativa.Id == id)
                    return tentativa;
            }
            return null;
        }

        public static int ObterId(int id)
        {
            return id;
        }
    }
}
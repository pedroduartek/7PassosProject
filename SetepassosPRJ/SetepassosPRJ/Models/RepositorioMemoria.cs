using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class RepositorioMemoria
    {

        private static TeamMember pereira = new TeamMember("comerestus", "João Pereira", 170323032);

        private static TeamMember pedro = new TeamMember("comerestus", "Pedro Duarte", 170323017);

        private static List<TeamMember> teamMembers = new List<TeamMember>() { pereira, pedro };

        private static List<Tentativa> tentativas = new List<Tentativa>();

        public static List<TeamMember> TeamMembers
        {
            get
            {
                return teamMembers;
            }
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
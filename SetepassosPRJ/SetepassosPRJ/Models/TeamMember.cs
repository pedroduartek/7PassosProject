using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class TeamMember
    {
        public string NomeDaEquipa { get; set; }
        public string NomeDoAluno { get; set; }
        public int NumeroDoAluno { get; set; }

        public TeamMember(string nomeDaEquipa, string nomeDoAluno, int numeroDoAluno) // nao tem retornar uma lista?
        {
            NomeDaEquipa = nomeDaEquipa;
            NomeDoAluno = nomeDoAluno;
            NumeroDoAluno = numeroDoAluno;
        }



    }
}

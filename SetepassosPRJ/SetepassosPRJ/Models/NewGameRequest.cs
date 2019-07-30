using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SetepassosPRJ.Models;

namespace SetepassosPRJ.Models
{
    public class NewGameRequest
    {
        public string PlayerName { get; set; }
        public string PlayerClass { get; set; }
        public string TeamKey { get; set; }


        public NewGameRequest(string playerName, string playerclass)
        {
            PlayerName = playerName;
            PlayerClass = playerclass;
            TeamKey = "24e126abc88e4065856f93d264cd4f4e";
        }
    }
}

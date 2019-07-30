using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public enum Playeraction { GoForward, GoBack, SearchArea, DrinkPotion, Attack, Flee, Quit }

    public class PlayRequest
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public Playeraction Playeraction { get; set; }

        public PlayRequest(int id, Playeraction playeraction)
        {
            ID = id;
            Key = "24e126abc88e4065856f93d264cd4f4e";
            Playeraction = playeraction;
        }


    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public enum Result { NoResult, Success, SuccessVictory, InvalidAction, GameHasEnded }

    public class GameState
    {
        public int GameId { get; set; }
        public int RoundNumber { get; set; }
        public Playeraction Action { get; set; }
        public Result Result { get; set; }
        public bool FoundEnemy { get; set; }
        public bool FoundItem { get; set; }
        public bool FoundKey { get; set; }
        public bool FoundPotion { get; set; }
        public int GoldFound { get; set; }
        public double EnemyDamageSuffered { get; set; }
        public double EnemyHealthPoints { get; set; }
        public int EnemyAttackPoints { get; set; }
        public int EnemyLuckPoints { get; set; }
        public int ItemHealthEffect { get; set; }
        public int ItemAttackEffect { get; set; }
        public int ItemLuckEffect { get; set; }
    }
}

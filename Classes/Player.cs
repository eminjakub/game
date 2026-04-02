using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Data.SQLite;
namespace game
{
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int money { get; set; }
        public int xp { get; set; }
        public int health { get; set; }
        public int strenght { get; set; }
        public int armor { get; set; }
        public int dexterity { get; set; }
        public int dungeon_lv { get; set; }

    }
}

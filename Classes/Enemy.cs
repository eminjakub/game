using System;
using System.Collections.Generic;
using System.Text;

namespace game
{
    public class Enemy
    {
        public string Name { get; set; }
        public int health { get; set; }
        public int max_health { get; set; }
        public int strenght { get; set; }
        public int armor { get; set; }
        public int dexterity { get; set; }
        public string ImagePath { get; set; }
        public int reward_money { get; set; }
        public int reward_xp { get; set; }

        public static List<Enemy> GetEnemies(int dungeonLv)
        {
            return new List<Enemy>
            {
                new Enemy
                {
                    Name = "Flákanec",
                    health = 60 + dungeonLv * 5,
                    max_health = 60 + dungeonLv * 5,
                    strenght = 3 + dungeonLv,
                    armor = 2 + dungeonLv,
                    dexterity = 1,
                    ImagePath = "./fotky/flakanec_postava.png",
                    reward_money = 80,
                    reward_xp = 5
                },
                new Enemy
                {
                    Name = "Zloděj",
                    health = 80 + dungeonLv * 8,
                    max_health = 80 + dungeonLv * 8,
                    strenght = 5 + dungeonLv * 2,
                    armor = 3 + dungeonLv,
                    dexterity = 4,
                    ImagePath = "./fotky/zlodej.png",
                    reward_money = 150,
                    reward_xp = 10
                },
                new Enemy
                {
                    Name = "Bezďák",
                    health = 120 + dungeonLv * 10,
                    max_health = 120 + dungeonLv * 10,
                    strenght = 8 + dungeonLv * 3,
                    armor = 6 + dungeonLv * 2,
                    dexterity = 2,
                    ImagePath = "./fotky/bezdak.png",
                    reward_money = 250,
                    reward_xp = 20
                }
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace game
{
    public class Fight
    {
        public Player player;
        public Enemy enemy;
        public int result { get; set; }
        public int playerHP { get; set; }
        public int playerMaxHP { get; set; }
        public List<string> battleLog { get; set; }
        public bool isFinished { get; set; }

        private Random random;

        public Fight(Player _player, Enemy _enemy)
        {
            this.player = _player;
            this.enemy = _enemy;
            this.random = new Random();
            this.battleLog = new List<string>();
            this.isFinished = false;
            this.result = 0;

            playerMaxHP = 80 + player.armor * 2;
            playerHP = playerMaxHP;

            battleLog.Add($"--- Souboj: Ty vs {enemy.Name} ---");
        }

        public string PlayerAttack()
        {
            if (isFinished) return "";

            int baseDmg = player.strenght + random.Next(1, 8);
            int reduction = enemy.armor / 3;
            int dmg = Math.Max(1, baseDmg - reduction);

            bool dodged = random.Next(1, 100) <= enemy.dexterity * 3;
            string msg;

            if (dodged)
            {
                msg = $"{enemy.Name} uhnul tvému útoku!";
            }
            else
            {
                enemy.health -= dmg;
                if (enemy.health < 0) enemy.health = 0;
                msg = $"Zasáhl jsi {enemy.Name} za {dmg} DMG! ({enemy.Name}: {enemy.health}/{enemy.max_health} HP)";
            }

            battleLog.Add(msg);

            if (enemy.health <= 0)
            {
                isFinished = true;
                result = 1;
                player.money += enemy.reward_money;
                player.xp += enemy.reward_xp;
                player.dungeon_lv++;
                string winMsg = $"Zvítězil jsi! Získáváš {enemy.reward_money} peněz a {enemy.reward_xp} XP!";
                battleLog.Add(winMsg);
                return msg + "\n" + winMsg;
            }

            return msg;
        }

        public string EnemyAttack()
        {
            if (isFinished) return "";

            int baseDmg = enemy.strenght + random.Next(1, 6);
            int reduction = player.armor / 3;
            int dmg = Math.Max(1, baseDmg - reduction);

            bool dodged = random.Next(1, 100) <= player.dexterity * 3;
            string msg;

            if (dodged)
            {
                msg = "Uhnul jsi nepřítelovu útoku!";
            }
            else
            {
                playerHP -= dmg;
                if (playerHP < 0) playerHP = 0;
                msg = $"{enemy.Name} tě zasáhl za {dmg} DMG! (Ty: {playerHP}/{playerMaxHP} HP)";
            }

            battleLog.Add(msg);

            if (playerHP <= 0)
            {
                isFinished = true;
                result = 2;
                string loseMsg = $"{enemy.Name} tě porazil!";
                battleLog.Add(loseMsg);
                return msg + "\n" + loseMsg;
            }

            return msg;
        }

        public string UsePotion(int healAmount)
        {
            if (isFinished) return "";

            int healed = Math.Min(healAmount, playerMaxHP - playerHP);
            playerHP += healed;
            string msg = $"Použil jsi lektvar! Obnoveno {healed} HP. (Ty: {playerHP}/{playerMaxHP} HP)";
            battleLog.Add(msg);
            return msg;
        }
    }
}

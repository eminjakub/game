using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game;
using SQLite;

namespace game
{
    using Threading = System.Threading.Tasks;

    public class Database
    {
        //SQLite connection
        private SQLiteAsyncConnection Conn;
        Player player { get; set; }

        public Database(string PathToDBFile)
        {
            Conn = new SQLiteAsyncConnection(PathToDBFile);
        }

        private async Threading.Task InitTables()
        {
             await Conn.CreateTableAsync<InventoryItem>();
        }

        // Inventory
        public async Threading.Task<List<InventoryItem>> GetInventory(int playerId)
        {
            await InitTables();
            return await Conn.Table<InventoryItem>().Where(i => i.PlayerId == playerId).ToListAsync();
        }

        public async Threading.Task<int> AddInventoryItem(InventoryItem item)
        {
            await InitTables();
            return await Conn.InsertAsync(item);
        }

        public async Threading.Task<int> RemoveInventoryItem(InventoryItem item)
        {
            await InitTables();
            return await Conn.DeleteAsync(item);
        }

        //Query
        public Threading.Task<List<Player>> GetItemsAsync()
        {
            return Conn.Table<Player>().ToListAsync();
        }
        
        public Threading.Task<List<Player>> SyncALL()
        {
            return Conn.QueryAsync<Player>("SELECT * FROM [player] where [id] = 1");
        }

        public List<Abstract_task> Taskddd(int id)
        {
            return Conn.QueryAsync<Abstract_task>($"SELECT * FROM [tasks] where [id] = {id}").Result;

        }

        public string Task_name(int id)
        {
            List<Task> nn = Conn.QueryAsync<Task>($"SELECT [task_name] FROM [tasks] where [id] = {id}").Result;
            return nn[0].Task_name;
        }
        public string Task_description(int id)
        {
            List<Task> nn = Conn.QueryAsync<Task>($"SELECT [task_description] FROM [tasks] where [id] = {id}").Result;
            return nn[0].task_description;
        }


        public string Task_time(int id)
        {
            List<Task> nn = Conn.QueryAsync<Task>($"SELECT [task_time] FROM [tasks] where [id] = {id}").Result;
            return nn[0].Task_lenght;
        }

        public string Task_money(int id)
        {
            List<Task> nn = Conn.QueryAsync<Task>($"SELECT [money] FROM [tasks] where [id] = {id}").Result;
            return nn[0].Task_money;
        }

        public string Task_xp(int id)
        {
            List<Task> nn = Conn.QueryAsync<Task>($"SELECT [xp] FROM [tasks] where [id] = {id}").Result;
            return nn[0].Task_xp;
        }

        public Threading.Task<int> Update(Player p1)
        {
            return Conn.UpdateAsync(p1);
        }

        public Threading.Task<List<Player>> SaveMoney(Player player)
        {
            int p_money = player.money;
            return Conn.QueryAsync<Player>($"UPDATE [player] SET [money] = {p_money} WHERE [id] = 1;");
            
        }
        public Threading.Task<List<Player>> SaveDungeonLv(Player player)
        {
            int p_dungeonlv = player.dungeon_lv;
            return Conn.QueryAsync<Player>($"UPDATE [player] SET [dungeon_lv] = {p_dungeonlv} WHERE [id] = 1;");

        }
        public Threading.Task<List<Player>> SaveXP(Player player)
        {
            int p_xp = player.xp;
            return Conn.QueryAsync<Player>($"UPDATE [player] SET [xp] = {p_xp} WHERE [id] = 1;");

        }
        public Threading.Task<List<Player>> SaveStrenght(Player player)
        {
            int p_strenght = player.strenght;
            return Conn.QueryAsync<Player>($"UPDATE [player] SET [strenght] = {p_strenght} WHERE [id] = 1;");

        }
        public Threading.Task<List<Player>> SaveDexterity(Player player)
        {
            int p_dexterity = player.dexterity;
            return Conn.QueryAsync<Player>($"UPDATE [player] SET [dexterity] = {p_dexterity} WHERE [id] = 1;");

        }
        public Threading.Task<List<Player>> SaveArmor(Player player)
        {
            int p_armor = player.armor;
            return Conn.QueryAsync<Player>($"UPDATE [player] SET [armor] = {p_armor} WHERE [id] = 1;");

        }
    }
}
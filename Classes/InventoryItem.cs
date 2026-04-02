using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace game
{
    public class InventoryItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsEquipped { get; set; }
        public string StatType { get; set; }
    }
}

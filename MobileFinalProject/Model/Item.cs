using System;
using SQLite;

namespace MobileFinalProject.Model

{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ItemName { get; set; }
        public bool IsNeeded { get; set; }
        public int SortOrder { get; set; }
        public int Category { get; set; }
        public bool IsChecked { get; set; }

    }
}
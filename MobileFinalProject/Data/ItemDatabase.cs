using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using MobileFinalProject.Model;
using AsyncAwaitBestPractices;

namespace MobileFinalProject.Data
{
    public class ItemDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public ItemDatabase()
        {
            InitializeAsync().SafeFireAndForget();
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == nameof(Item)))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Item)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public Task<List<Item>> GetItemsAsync()
        {
            return Database.Table<Item>().Where(x => x.IsNeeded).OrderBy(x => x.IsChecked).ToListAsync();
        }
        public Task<List<Item>> GetAllItemsAsync()
        {
            return Database.Table<Item>().OrderBy(x => x.ItemName).ToListAsync();

        }

        public Task<Item> GetItemAsync(int id)
        {
            var x = Database.Table<Item>().Where(i => i.ID == id).FirstOrDefaultAsync();
            return x;
        }

        public Task<int> SaveItemAsync(Item item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> InsertList(IEnumerable<Item> items)
        {
            return Database.InsertAllAsync(items);
        }

        public Task<int> DeleteItemAsync(Item item)
        {
            return Database.DeleteAsync(item);
        }
        public Task<int> ClearAllAsync()
        {
            return Database.DeleteAllAsync<Item>();
        }
        public Task<int> UpdateList(IEnumerable<Item> items)
        {
            return Database.UpdateAllAsync(items);
        }

    }
}

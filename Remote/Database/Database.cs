using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Remote.Database
{
    class Database
    {
        protected DatabaseSchema databaseSchema { get; set; }
        private string databasePath { get; set; } = Directory.GetCurrentDirectory() + @"\database.json";

        protected Database()
        {
            LoadDatabase();
            databaseSchema.users.ListChanged += (s, e) => SaveDatabase();
        }

        ~Database()
        {
            SaveDatabase();
        }

        protected void LoadDatabase()
        {
            databaseSchema = JsonConvert.DeserializeObject<DatabaseSchema>(File.ReadAllText(databasePath));
        }

        protected void SaveDatabase()
        {
            File.WriteAllText(databasePath, JsonConvert.SerializeObject(databaseSchema));
        }
    }
}

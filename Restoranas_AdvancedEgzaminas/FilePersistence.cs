using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Restoranas_AdvancedEgzaminas
{
    public class FilePersistence
    {
        public void WriteToFile(List<DishGroup> dishGroups)
        {
            string jsonString = JsonSerializer.Serialize(dishGroups, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("DishGroups.json", jsonString);
        }

        public List<DishGroup> ReadDishGroupsFromFile()
        {
            string jsonString = File.ReadAllText("DishGroups.json");
            return JsonSerializer.Deserialize<List<DishGroup>>(jsonString);
        }
    }
}

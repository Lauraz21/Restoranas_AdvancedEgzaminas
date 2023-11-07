using System;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Restoranas_AdvancedEgzaminas
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Table> tables = new List<Table>();

            for (int i = 1; i <= 10; i++)
            {
                tables.Add(new Table() { ID = i });
            }

            FilePersistence filePersistence = new FilePersistence();

            List<DishGroup> dishGroups = filePersistence.ReadDishGroupsFromFile();

            ConsoleMenu menu = new ConsoleMenu(new SimpleReceiptWriter());

            menu.ShowMenu(tables, dishGroups);
        }
    }
}
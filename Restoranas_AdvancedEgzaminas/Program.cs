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
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Table> tables = new List<Table>();

            for (int i = 1; i <= 10; i++)
            {
                tables.Add(new Table() { ID = i });
            }

            List<DishGroup> dishGroups = ReadDishGroupsFromFile();

            ShowMenu(tables, dishGroups);
        }
        public static void ShowMenu(List<Table> tables, List<DishGroup> dishGroups)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" ORDERING SYSTEM");
                Console.WriteLine(" \n (1) MAKE ORDER \n (2) RECEIPT \n (3) FREE TABLE \n (q) QUIT");

                char choice = Console.ReadKey().KeyChar;
             
                switch (choice)
                {
                    case '1':
                        MakeOrder(tables, dishGroups);
                        break;

                    case '2':
                        ShowReceipt(tables);
                        break;
                    case '3':
                        FreeTable(tables);
                        break;

                    case 'q':
                        return;
                }
            }
        }
        public static void MakeOrder(List<Table> tables, List<DishGroup> dishGroups)
        {
            Console.Clear();
            Console.WriteLine("SELECT A TABLE FROM 1 TO 10");
            int selectedTableID = int.Parse(Console.ReadLine());
           
            Table selectedTable = tables.First(table => table.ID == selectedTableID);

            if (selectedTable.IsReserved)
            {
                Console.WriteLine("Warning. Table is already reserved, editing order.");
            }
            else
            {
                selectedTable.ReserveTable();
            }
            SelectGroup(dishGroups, selectedTable);
        }

        public static void SelectGroup(List<DishGroup> dishGroups, Table table)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("SELECT A GROUP: ");

                foreach (var dishGroup in dishGroups)
                {
                    Console.WriteLine($"({dishGroup.ID}){dishGroup.Name}");
                }

                Console.WriteLine("q - BACK");

                string userInput = Console.ReadLine();

                if (userInput == "q")
                {
                    return;
                }
                int selectedDishGroupID = int.Parse(userInput);
               

                DishGroup selectedGroup = dishGroups.First(dishGroup => dishGroup.ID == selectedDishGroupID);

                SelectDish(selectedGroup.Dishes, table);
            }
        }
        public static void SelectDish(List<Dish> dishes, Table table)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("SELECT DISH");

                foreach (Dish dish in dishes)
                {
                    Console.WriteLine($"({dish.ID}){dish.Name}");
                }

                Console.WriteLine("(q) - BACK");

                string userInput = Console.ReadLine();
                    
                if (userInput == "q")
                {
                    return;
                }
                int selectedDishID = int.Parse(userInput);
               

                Dish selectedDish = dishes.First(dish => dish.ID == selectedDishID);

                table.OrderDishes.Add(selectedDish);
            }
        }
        public static void ShowReceipt (List<Table> tables)
        {
            Console.Clear ();
            Console.WriteLine("SELECT TABLE: ");
            int selectedTableID = int.Parse(Console.ReadLine());
            Table selectedTable = tables.First(table => table.ID == selectedTableID);

            if (!selectedTable.IsReserved)
            {
                Console.WriteLine("NO ORDER");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"TIME: {selectedTable.ReservationTime}");
            foreach (Dish dish in selectedTable.OrderDishes)
            {
                Console.WriteLine($"{dish.Name} {dish.Price} €");
            }

            Console.WriteLine($"TOTAL: {selectedTable.GetOrderTotal()} €");

            Console.ReadKey();
        }

        public static void FreeTable(List<Table> tables)
        {

            Console.Clear();
            Console.WriteLine("SELECT TABLE: ");
            int selectedTableID = int.Parse(Console.ReadLine());
            Table selectedTable = tables.First(table => table.ID == selectedTableID);


            selectedTable.FreeTable();
            Console.WriteLine("TABLE FREED");
            Console.ReadKey();
        }


        public static void WriteToFile(List<DishGroup> dishGroups)
        {
            string jsonString = JsonSerializer.Serialize(dishGroups, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("DishGroups.json", jsonString);
        }

        public static List<DishGroup> ReadDishGroupsFromFile()
        {
            string jsonString = File.ReadAllText("DishGroups.json");
            return JsonSerializer.Deserialize<List<DishGroup>>(jsonString);
        }
    }
}
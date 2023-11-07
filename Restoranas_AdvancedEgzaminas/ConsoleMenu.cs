using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoranas_AdvancedEgzaminas
{
    public class ConsoleMenu
    {
        private IReceiptWriter _receiptWriter;
        public ConsoleMenu(IReceiptWriter receiptWriter) 
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            _receiptWriter = receiptWriter;
        }

        public void ShowMenu(List<Table> tables, List<DishGroup> dishGroups)
        {
            while (true)
            {
                Console.Clear();
               
                Console.WriteLine(" ORDERING SYSTEM ");
                Console.WriteLine(" \n(1) ORDER \n(2) RECEIPT \n(3) FREE TABLE \n(q) QUIT");

                char choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        MakeOrder(tables, dishGroups);
                        break;

                    case '2':
                        _receiptWriter.WriteReceipt(tables);
                        break;
                    case '3':
                        FreeTable(tables);
                        break;

                    case 'q':
                        return;
                }
            }
        }
        private void MakeOrder(List<Table> tables, List<DishGroup> dishGroups)
        {
            Console.Clear();
            Console.WriteLine("SELECT A TABLE FROM 1 TO 10");
            int selectedTableID = int.Parse(Console.ReadLine());

            Table selectedTable = tables.First(table => table.ID == selectedTableID);

            if (selectedTable.IsReserved)
            {
                Console.WriteLine("WARNING! THE TABLE IS ALREADY RESERVED, EDITING ORDER: ");
            }
            else
            {
                selectedTable.ReserveTable();
            }
            SelectGroup(dishGroups, selectedTable);
        }

        private void SelectGroup(List<DishGroup> dishGroups, Table table)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("SELECT A GROUP: \n");

                foreach (var dishGroup in dishGroups)
                {
                    Console.WriteLine($"({dishGroup.ID}) {dishGroup.Name}");
                }

                Console.WriteLine("(q) BACK");

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
        private void SelectDish(List<Dish> dishes, Table table)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("SELECT DISH: \n");

                foreach (Dish dish in dishes)
                {
                    Console.WriteLine($"({dish.ID}) {dish.Name}");
                }

                Console.WriteLine("(q) BACK");

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
        private void FreeTable(List<Table> tables)
        {
            Console.Clear();
            Console.WriteLine("SELECT TABLE: ");
            int selectedTableID = int.Parse(Console.ReadLine());
            Table selectedTable = tables.First(table => table.ID == selectedTableID);


            selectedTable.FreeTable();
            Console.WriteLine("TABLE FREED");
            Console.ReadKey();
        }
    }
}

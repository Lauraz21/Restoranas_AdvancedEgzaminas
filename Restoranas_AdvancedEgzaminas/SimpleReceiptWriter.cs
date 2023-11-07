﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoranas_AdvancedEgzaminas
{
    public class SimpleReceiptWriter : IReceiptWriter
    {
        public void WriteReceipt(List<Table> tables)
        {
            Console.Clear();
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
    }
}
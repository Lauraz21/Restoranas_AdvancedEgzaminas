using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoranas_AdvancedEgzaminas
{
    public class Table
    {
        public int ID { get; set; }
        public List<Dish> OrderDishes { get; set; }
        public DateTime ReservationTime { get; set; }
        public bool IsReserved { get; set; }

        public void ReserveTable()
        {
            ReservationTime = DateTime.Now;
            IsReserved = true;
            OrderDishes = new List<Dish>();
        }
        public void FreeTable()
        {
            IsReserved = false;
        }
        public double GetOrderTotal()
        {
            return OrderDishes.Sum(dish => dish.Price);
        }
    }
}

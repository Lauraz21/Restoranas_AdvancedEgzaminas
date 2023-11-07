using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoranas_AdvancedEgzaminas
{
    public class DishGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();
    }
}

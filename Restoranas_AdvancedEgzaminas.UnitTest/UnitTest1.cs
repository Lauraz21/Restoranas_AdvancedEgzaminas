using System.ComponentModel.DataAnnotations.Schema;

namespace Restoranas_AdvancedEgzaminas.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestOrderTotal()
        {
            //actual
            Table actualTable = new Table();
            Dish dish1 = new Dish()
            {
                Price = 5.6M,
            };
            actualTable.OrderDishes.Add(dish1);

            Dish dish2 = new Dish()
            {
                Price = 7.2M,
            };
            actualTable.OrderDishes.Add(dish2);

            //expected
            decimal expectedTotal = 12.8M;

            //Assert
            Assert.AreEqual(expectedTotal, actualTable.GetOrderTotal());
        }
    }
}
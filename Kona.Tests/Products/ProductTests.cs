using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Kona.Model;
using System.Linq.Expressions;
using Kona.Infrastructure;
using Kona.Web.App.Controllers;
using System.Web.Mvc;
using Xunit;

namespace Commerce.DomainTests.Products {
    /// <summary>
    /// Summary description for ProductModel
    /// </summary>
    
    public class ProductTests {

        Product GetTestProduct() {
            var result = new Product("SKU", "Name", true, 100, 1, DateTime.Now, true, true);
            result.Price = 10M;
            return result;
        }

        [Fact]
        public void ProductModel_ShouldHave_Zero_DiscountPercent() {

            Product p = GetTestProduct();

            Assert.Equal(0, p.DiscountPercent);

        }
        [Fact]
        public void ProductModel_Should_Return_9_For_DiscountedPrice_With_10_For_DiscountPercent() {

            Product p = GetTestProduct();
            p.DiscountPercent = 10;
            Assert.Equal(9, p.DiscountedPrice);

        }

        [Fact]
        public void ProductModel_Should_Consider_SKU_Equality() {

            Product p = GetTestProduct();
            Product p2 = GetTestProduct();
            Assert.True(p.Equals(p2));

        }

        [Fact]
        public void ProductModel_Should_Return_Name_For_To_String() {

            Product p = GetTestProduct();
            Assert.Equal("Name", p.ToString());

        }
        [Fact]
        public void ProductModel_Should_Set_Inventory_State_To_InStock_With_10_OnHand_And_DateAvailable_Now() {

            Product p = new Product("TEST","test",true, 10, 1, DateTime.Now.AddDays(-7), true,true);
            Assert.IsType(typeof(InStock), p.CurrentInventory);
        }
        [Fact]
        public void ProductModel_Should_Set_Inventory_State_To_PreOrder_With_0_OnHand_DateAvailable_InFuture() {

            Product p = new Product("TEST", "test", true, 10, 0, DateTime.Now.AddDays(7), true, true);
            p.AllowPreOrder = true;
            Assert.IsType(typeof(OnPreOrder),p.CurrentInventory );
        }

        [Fact]
        public void ProductModel_Should_Set_Inventory_State_To_BackOrder_With_0_OnHand_DateAvailable_Now_And_AllowbackOrder() {

            Product p = new Product("TEST", "test", true, 10, 0, DateTime.Now.AddDays(-7), true, true);
            Assert.IsType(typeof(OnBackOrder), p.CurrentInventory);
        }

        [Fact]
        public void ProductModel_Should_Set_Inventory_State_To_Unavailable_With_0_OnHand_DateAvailable_And_Not_AllowBackOrder() {

            Product p = new Product("TEST", "test", true, 10, 0, DateTime.Now.AddDays(-7), false, true);
            Assert.IsType(typeof(Unavailable), p.CurrentInventory);
        }

        [Fact]
        public void ProductModel_Should_Set_Inventory_State_To_PreOrder_With_10_OnHand_DateAvailable_Future() {

            Product p = new Product("TEST", "test", true, 10, 10, DateTime.Now.AddDays(7), false, true);
            p.AllowPreOrder = true;
            Assert.IsType(typeof(OnPreOrder), p.CurrentInventory);
        }
    }
}

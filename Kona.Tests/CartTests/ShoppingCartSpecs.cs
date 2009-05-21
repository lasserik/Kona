using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kona.Data;
using Machine.Specifications;

namespace Kona.Tests.CartTests {


/*

Empty Cart
Adding an Item
* TotalItems is incremented by Quantity

Cart with >0 items
Adding an item
  * When same item is added, Quantity is updated, TotalItems incremented by Quantity, and 
    Items.Count remains the same
  * When new item added, TotalItems is incremented by Quantity
  * When quantity is 0, nothing happens

Removing an Item
  * All items of same SKU are removed, TotalItems decremented by Quantity
  * When quantity negative, items are removed, TotalItems stays at 0 (never negative)

Adjusting Quantity
  * Quantity of items is set to quantity passed in, 
  * TotalItems adjusted to reflect difference
  * If adjustment is 0, nothing happens
  * If SKU isn't found, nothing happens
  * If adjustment is negative TotalItems is decremented
  * If negative adjustment is greater than Quantity, item is removed

     
*/

    [Subject("Empty Shopping Cart")]
    public class when_adding_an_item : with_empty_cart {

        Because of = () => {
            cart.AddItem(new TestProduct(), 1);
        };

        It should_increment_totalitems_by_quantity = () => {
            cart.TotalItems.ShouldEqual(1);
        };

    }
    [Subject("Empty Shopping Cart")]
    public class when_adding_an_item_with_0_quantity : with_empty_cart {

        Because of = () => {
            cart.AddItem(new TestProduct(), 0);
        };

        It should_increment_totalitems_by_quantity = () => {
            cart.TotalItems.ShouldEqual(0);
        };

    }
    
    [Subject("Cart with items in it")]
    public class when_same_item_added : with_cart_with_1_item_of_sku1 {

        Because of = () => {
            cart.AddItem(new TestProduct());
        };


        It should_update_quantity_when_same_item_added = () => {
            cart.Items[0].Quantity.ShouldEqual(2);
        };
        It should_increment_total_items_by_quantity = () => {
            cart.TotalItems.ShouldEqual(2);
        };
        It should_have_only_one_item_in_list = () => {
            cart.Items.Count.ShouldEqual(1);
        };
    }
    [Subject("Cart with items in it")]
    public class when_removing_item : with_cart_with_1_item_of_sku1 {


        Because of = () => {
            cart.RemoveItem("SKU1");
        };


        It should_remove_all_items_with_same_sku = () => {
            cart.TotalItems.ShouldEqual(0);
        };



    }
    [Subject("Cart with items in it")]
    public class when_removing_item_with_bad_sku : with_cart_with_1_item_of_sku1 {
        Because of = () => {
            cart.RemoveItem("SKU2");
        };

        It should_not_remove_anything_when_sku_not_in_cart = () => {
            cart.TotalItems.ShouldEqual(1);
        };

    }
    [Subject("Cart with items in it")]
    public class when_adjusting_quantity_to_100 : with_cart_with_1_item_of_sku1 {

        Because of = () => {
            cart.AdjustQuantity("SKU1", 100);
        };


        It should_adjust_quantity_of_item_with_matching_sku_to_quantity_passed_in = () => {
            cart.Items[0].Quantity.ShouldEqual(100);
        };
        It should_adjust_total_items_to_equal_sum_of_new_item_quantities = () => {
            cart.TotalItems.ShouldEqual(100);

        };

    }
    [Subject("Cart with items in it")]
    public class when_adjusting_quantity_with_bad_sku : with_cart_with_1_item_of_sku1 {
        Because of = () => {
            cart.AdjustQuantity("SKU2", 100);
        };
        It should_not_adjust_totalitems_when_sku_not_found = () => {
            cart.TotalItems.ShouldEqual(1);
        };

    }

    [Subject("Cart with items in it")]
    public class when_adjusting_quantity_to_0 : with_cart_with_1_item_of_sku1 {
        Because of = () => {
            cart.AdjustQuantity("SKU1", 0);
        };
        It should_remove_item = () => {
            cart.TotalItems.ShouldEqual(0);
        };

    }
    [Subject("Cart with items in it")]
    public class when_adjusting_negative_quantity : with_cart_with_1_item_of_sku1 {
        Because of = () => {
            cart.AdjustQuantity("SKU1", -2);
        };
        It should_decrement_if_adjustment_is_negative = () => {
            cart.TotalItems.ShouldEqual(0);
        };
        It should_not_decrement_below_0 = () => {
            cart.TotalItems.ShouldEqual(0);
        };

    }


    public abstract class with_empty_cart {
        protected static ShoppingCart cart;
        Establish context = () => {
            cart = new ShoppingCart();
        };

    }
    public abstract class with_cart_with_1_item_of_sku1 {
        protected static ShoppingCart cart;
        Establish context = () => {
            cart = new ShoppingCart();
            cart.AddItem(new TestProduct());
        };

    }

    public class TestProduct:Product {
        public TestProduct() {
            this.SKU = "SKU1";
            this.BasePrice = 100;
        }
    }

}

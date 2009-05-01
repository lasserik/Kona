using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kona.Model;
using Kona.Infrastructure;

namespace Tests.Mocks {
    public class MockProductRepository:IProductRepository {

        List<Product> productList;
        List<Image> imageList;
        List<ProductDescriptor> descriptorList;

        public MockProductRepository() {
            
            //load up the repo list
            productList = new List<Product>();
            imageList = new List<Image>();
            descriptorList = new List<ProductDescriptor>();

            for(int i=1;i<=3;i++){
                imageList.Add(new Image("noimage.gif","noimage.gif"));
            }
            
            for(int i=1;i<=3;i++){
                descriptorList.Add(new ProductDescriptor("title","Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec non diam eu tellus elementum ullamcorper. Maecenas leo magna, tempor eget, molestie quis, ornare sed, ligula. Etiam purus nisl, tempus a, commodo quis, posuere quis, elit. Nunc urna nisi, varius ut, hendrerit ut, bibendum in, ipsum."));
            }


            for (int i = 1; i <= 5; i++) {

                Product p =new Product("SKU" + i,"Product "+i,true,10);
                p.WeightInPounds = 5;


                //set first three products to shipped
                p.Delivery = i <= 3 ? DeliveryMethod.Shipped : DeliveryMethod.Download;


                //set first three products to Back-orderable
                p.AllowBackOrder = i <= 3;

                //set the 2nd product to BackOrder
                p.Inventory = i == 2 ? InventoryStatus.BackOrder : InventoryStatus.InStock;

                //set all products to taxable, except the 5th
                p.IsTaxable = i != 5;

                p.Recommended = new List<Product>();

                //have it recommend itself, for now
                p.Recommended.Add(p);

                //related
                p.RelatedProducts = new List<Product>();
                for (int x = 0; x < 5; x++)
			    {
                    p.RelatedProducts.Add(new Product("SKU_REL" + x));

                }

                //add some Crosses
                p.CrossSells = new LazyList<Product>();
                for (int x = 0; x < 5; x++)
			    {
                    p.CrossSells.Add(new Product("SKU_CROSS" + x));

                }
                productList.Add(p);
            }



        }

        public IList<Product> GetProducts() {
            return productList;
        }

        public void Save(Product p) {
            productList.Add(p);
        }
        public Product GetProduct(string sku) {
            return productList.Where(x => x.SKU == sku).SingleOrDefault();
        }


    }
}

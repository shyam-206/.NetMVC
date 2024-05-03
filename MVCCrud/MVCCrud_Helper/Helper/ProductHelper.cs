using MVCCrud_Model.Context;
using MVCCrud_Model.CustomModel;
using System;
using System.Collections.Generic;

namespace MVCCrud_Helper.Helper
{
    public class ProductHelper
    {
        public static List<ProductModel> GetProductByHelper(List<Product> productList)
        {
            try
            {

                List<ProductModel> productModelList = new List<ProductModel>();
                if (productList != null)
                {
                    foreach (var prod in productList)
                    {
                        ProductModel product = new ProductModel();
                        product.Product_Id = prod.Product_Id;
                        product.PName = prod.PName;
                        product.Price = prod.Price;
                        product.Quantity = prod.Quantity;
                        productModelList.Add(product);
                    }
                }
                return productModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Product ConvertProductModelToProduct(ProductModel productModel)
        {
            try
            {

                Product product = new Product();

                if (productModel != null)
                {
                    product.Product_Id = productModel.Product_Id;
                    product.PName = productModel.PName;
                    product.Price = productModel.Price;
                    product.Quantity = productModel.Quantity;
                }

                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static ProductModel ConvertProductToProductModel(Product product)
        {
            try
            {

                ProductModel productModel = new ProductModel();
                if (product != null)
                {
                    productModel.Product_Id = product.Product_Id;
                    productModel.PName = product.PName;
                    productModel.Price = product.Price;
                    productModel.Quantity = product.Quantity;
                }

                return productModel;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

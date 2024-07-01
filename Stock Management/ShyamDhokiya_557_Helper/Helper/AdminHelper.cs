using ShyamDhokiya_557_Model.DbContext;
using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Helper.Helper
{
    public class AdminHelper
    {
        public static Product ConvertProductModelToProduct(ProductModel producModel)
        {
            try
            {
                Product product = new Product();
                if(producModel != null)
                {
                    product.ProductId = producModel.ProductId;
                    product.Name = producModel.Name;
                    product.Descrption = producModel.Descrption;
                    product.Type = producModel.Type;
                    product.Price = producModel.Price;
                    product.Quantity = producModel.Quantity;
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
                    productModel.ProductId = product.ProductId;
                    productModel.Name = product.Name;
                    productModel.Descrption = product.Descrption;
                    productModel.Type = product.Type;
                    productModel.Price = (int)product.Price;
                    productModel.Quantity = (int)product.Quantity;
                }

                return productModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<ProductModel> ConvertProductListToList(List<Product> products)
        {
            try
            {
                List<ProductModel> productModels = new List<ProductModel>();
                if(products != null)
                {
                    foreach(var product in products)
                    {
                        ProductModel productModel = new ProductModel();
                        productModel.ProductId = product.ProductId;
                        productModel.Name = product.Name;
                        productModel.Descrption = product.Descrption;
                        productModel.Type = product.Type;
                        productModel.Price = (int)product.Price;
                        productModel.Quantity = (int)product.Quantity;
                        productModels.Add(productModel);
                    }
                }

                return productModels;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<UserModel> ConvertUsersToList(List<Suppiers> suppiers)
        {
            try
            {
                List<UserModel> userModels = new List<UserModel>();
                foreach(var item in suppiers)
                {
                    UserModel userModel = new UserModel();
                    userModel.UserId = item.SuppierId;
                    userModel.Name = item.Name;
                    userModel.Email = item.Email;
                    userModel.Password = item.Password;
                    userModels.Add(userModel);
                }

                return userModels;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}

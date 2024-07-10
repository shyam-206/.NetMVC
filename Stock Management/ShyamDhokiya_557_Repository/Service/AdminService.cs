using ShyamDhokiya_557_Helper.Helper;
using ShyamDhokiya_557_Model.DbContext;
using ShyamDhokiya_557_Model.ViewModel;
using ShyamDhokiya_557_Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Repository.Service
{
    public class AdminService : AdminRepository
    {
        private readonly StockManagement_557Entities1 db = new StockManagement_557Entities1();

        public bool Addproduct(ProductModel productModel)
        {
            try
            {
                int save = 0;
                Product product = new Product();
                if (productModel != null)
                {
                    product = AdminHelper.ConvertProductModelToProduct(productModel);
                    db.Product.Add(product);
                    save = db.SaveChanges();
                }

                return save > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteProduct(int ProductId)
        {
            try
            {
                int delete = 0;
                Product product = db.Product.Where(m => m.ProductId == ProductId).FirstOrDefault();
                product.IsDelete = true;
                delete = db.SaveChanges();
                List<CartDetail> cartDetails = db.CartDetail.Where(m => m.ProductId == product.ProductId).ToList();
                db.CartDetail.RemoveRange(cartDetails);
                delete = db.SaveChanges();
                return delete > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EditProduct(ProductModel productModel)
        {
            try
            {
                int edit = 0;
                Product product = new Product();
                product = AdminHelper.ConvertProductModelToProduct(productModel);
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                edit = db.SaveChanges();
                return edit > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<OrderTableModel> FilterData(DateTime StartDate, DateTime EndDate, int ProductName)
        {
            try
            {
                List<OrderTableModel> list = db.Database.SqlQuery<OrderTableModel>("exec OrderTableList").ToList();
                EndDate = EndDate.Date.AddDays(1).AddTicks(-1);
                if (ProductName != 0)
                {
                    List<OrderTableModel> FilterList = list.Where(m => m.OrderDate >= StartDate && m.OrderDate <= EndDate && m.ProductId == ProductName).ToList();
                    return FilterList;
                }
                else
                {
                    List<OrderTableModel> FilterList = list.Where(m => m.OrderDate >= StartDate && m.OrderDate <= EndDate).ToList();
                    return FilterList;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public ProductModel GetProductById(int ProductId)
        {
            try
            {
                Product product = new Product();
                ProductModel productModel = new ProductModel();
                product = db.Product.FirstOrDefault(m => m.ProductId == ProductId);
                if (product != null)
                {
                    productModel = AdminHelper.ConvertProductToProductModel(product);
                }

                return productModel != null ? productModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ProductModel> GetProductList()
        {
            try
            {
                List<ProductModel> productModelList = new List<ProductModel>();
                List<Product> products = db.Product.Where(m => m.IsDelete != true).ToList();
                if (products != null)
                {
                    productModelList = AdminHelper.ConvertProductListToList(products);
                }

                return productModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UserModel> GetSuppierList()
        {
            try
            {
                List<UserModel> userModelList = new List<UserModel>();
                List<Suppiers> suppiers = db.Suppiers.ToList();
                if (suppiers != null)
                {
                    userModelList = AdminHelper.ConvertUsersToList(suppiers);
                }

                return userModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<OrderTableModel> OrderList()
        {
            try
            {
                List<OrderTableModel> orderList = new List<OrderTableModel>();
                orderList = db.Database.SqlQuery<OrderTableModel>("exec OrderTableList").ToList();
                return orderList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}

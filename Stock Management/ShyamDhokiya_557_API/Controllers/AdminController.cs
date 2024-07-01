using ShyamDhokiya_557_API.JWTAuthencation;
using ShyamDhokiya_557_Model.ViewModel;
using ShyamDhokiya_557_Repository.Repository;
using ShyamDhokiya_557_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShyamDhokiya_557_API.Controllers
{
    [JwtAuthencation]
    public class AdminController : ApiController
    {
        private readonly AdminRepository repo = new AdminService();

        [Route("api/AdminAPI/Addproduct")]
        [HttpPost]
        public bool Addproduct(ProductModel productModel)
        {
            try
            {
                bool addProduct = repo.Addproduct(productModel);
                return addProduct;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/AdminAPI/EditProduct")]
        [HttpPost]
        public bool EditProduct(ProductModel productModel)
        {
            try
            {
                bool editSave = repo.EditProduct(productModel);
                return editSave;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/AdminAPI/GetProductById")]
        [HttpGet]
        public ProductModel GetProductById(int ProductId)
        {
            try
            {
                ProductModel productModel = repo.GetProductById(ProductId);
                return productModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/AdminAPI/GetSuppierList")]
        [HttpGet]
        public List<UserModel> GetSuppierList()
        {
            try
            {
                List<UserModel> list = repo.GetSuppierList();
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/AdminAPI/GetProductList")]
        [HttpGet]
        public List<ProductModel> GetProductList()
        {
            try
            {
                List<ProductModel> list = repo.GetProductList();
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/AdminAPI/DeleteProduct")]
        [HttpGet]
        public bool DeleteProduct(int ProductId)
        {
            try
            {
                bool delete = repo.DeleteProduct(ProductId);
                return delete;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/AdminAPI/OrderList")]
        [HttpGet]
        public List<OrderModel> OrderList()
        {
            try
            {
                List<OrderModel> list = repo.OrderList();
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/AdminAPI/SortOrderListByDate")]
        [HttpGet]
        public List<OrderModel> SortOrderListByDate(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                List<OrderModel> List = repo.SortOrderListByDate(StartDate, EndDate);
                return List;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
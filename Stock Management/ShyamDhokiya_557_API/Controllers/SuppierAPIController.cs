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
    public class SuppierAPIController : ApiController
    {
        private readonly SuppierRepository repo = new SuppierService();
        [Route("api/SuppierAPI/GetProductList")]
        [HttpGet]
        public List<ProductModel> GetProductList(int SuppierId)
        {
            try
            {
                List<ProductModel> list = repo.GetProductList(SuppierId);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/SuppierAPI/GetCartItemList")]
        [HttpGet]
        public List<CartDetailModel> GetCartItemList(int SuppierId)
        {
            try
            {
                List<CartDetailModel> list = repo.GetCartItemList(SuppierId);
                return list != null ? list : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/SuppierAPI/AddCart")]
        [HttpGet]
        public bool AddCart(int ProductId, int SuppierId)
        {
            try
            {
                bool SaveCart = repo.AddCart(ProductId, SuppierId);
                return SaveCart;
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }

        [Route("api/SuppierAPI/IncreaseQuantity")]
        [HttpGet]
        public bool IncreaseQuantity(int CartDetailId)
        {
            try
            {
                bool increaseQuantity = repo.IncreaseQuantity(CartDetailId);
                return increaseQuantity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/SuppierAPI/DecreaseQuantity")]
        [HttpGet]
        public bool DecreaseQuantity(int CartDetailId)
        {
            try
            {
                bool DecreaseQuantity = repo.DecreaseQuantity(CartDetailId);
                return DecreaseQuantity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/SuppierAPI/RemoveCart")]
        [HttpGet]
        public bool RemoveCart(int CartDetailId, int SuppierId)
        {
            try
            {
                bool RemoveCart = repo.RemoveCart(CartDetailId, SuppierId);
                return RemoveCart;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/SuppierAPI/AddOrder")]
        [HttpGet]
        public bool AddOrder(int SuppierId)
        {
            try
            {
                bool AddOrder = repo.AddOrder(SuppierId);
                return AddOrder;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/SuppierAPI/OrderList")]
        [HttpGet]
        public List<OrderModel> OrderList(int SuppierId)
        {
            try
            {
                List<OrderModel> OrderList = repo.OrderList(SuppierId);
                return OrderList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
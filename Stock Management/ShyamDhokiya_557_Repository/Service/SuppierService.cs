using ShyamDhokiya_557_Helper.Helper;
using ShyamDhokiya_557_Model.DbContext;
using ShyamDhokiya_557_Model.ViewModel;
using ShyamDhokiya_557_Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Repository.Service
{
    public class SuppierService : SuppierRepository
    {
        private readonly StockManagement_557Entities db = new StockManagement_557Entities();

        public bool AddCart(int ProductId, int SuppierId)
        {
            try
            {
                int SaveCart = 0;
                Cart cart = db.Cart.Where(m => m.SuppierId == SuppierId).FirstOrDefault();
                Product product = db.Product.Where(m => m.ProductId == ProductId).FirstOrDefault();
                CartDetail cartDetail = new CartDetail();
                if (product != null && cart != null)
                {
                    cartDetail.CartId = cart.CartId;
                    cartDetail.ProductId = product.ProductId;
                    cartDetail.Quantity = 1;
                    cartDetail.IsAdd = true;
                    db.CartDetail.Add(cartDetail);
                    SaveCart = db.SaveChanges();
                }

                return SaveCart > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool AddOrder(int SuppierId)
        {
            try
            {
                int AddOrder = 0;
                Cart cart = db.Cart.Where(m => m.SuppierId == SuppierId).FirstOrDefault();
                List<CartDetail> cartDetails = db.CartDetail.Where(m => m.CartId == cart.CartId).ToList();

                if (cart != null)
                {
                    Orders order = new Orders()
                    {
                        SuppierId = SuppierId,
                        OrderDate = DateTime.Now,
                        OrderTotal = OrderTotalPrice(cartDetails)
                    };
                    Orders saveOrder = db.Orders.Add(order);
                    AddOrder = db.SaveChanges();

                    foreach (var item in cartDetails)
                    {
                        Product product = db.Product.Where(m => m.ProductId == item.ProductId).FirstOrDefault();
                        if (product != null && product.Quantity >= item.Quantity)
                        {
                            OrderDetail orderDetail = new OrderDetail()
                            {
                                OrderId = saveOrder.OrderId,
                                ProductId = product.ProductId,
                                Quantity = item.Quantity,
                                Price = (item.Quantity * product.Price)
                            };
                            product.Quantity = product.Quantity - item.Quantity;
                            AddOrder = db.SaveChanges();
                            db.OrderDetail.Add(orderDetail);
                            AddOrder = db.SaveChanges();
                            db.CartDetail.Remove(item);
                            AddOrder = db.SaveChanges();
                        }
                    }
                }

                return AddOrder > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int OrderTotalPrice(List<CartDetail> cartDetails)
        {
            try
            {
                int total = 0;
                foreach (var item in cartDetails)
                {
                    Product product = db.Product.Where(m => m.ProductId == item.ProductId).FirstOrDefault();
                    total = total + (int)(item.Quantity * product.Price);
                }
                return total;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DecreaseQuantity(int CartDetailId)
        {
            try
            {
                int Des = 0;
                CartDetail cartDetail = db.CartDetail.Where(m => m.CartDetailId == CartDetailId).FirstOrDefault();
                Product product = db.Product.Where(m => m.ProductId == cartDetail.ProductId).FirstOrDefault();
                if (cartDetail != null && cartDetail.Quantity > 1 && product.Quantity > 1)
                {
                    cartDetail.Quantity = cartDetail.Quantity - 1;
                    Des = db.SaveChanges();
                }

                return Des > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CartDetailModel> GetCartItemList(int SuppierId)
        {
            try
            {
                List<CartDetailModel> list = new List<CartDetailModel>();
                List<CartDetail> cartDetails = db.CartDetail.Where(m => m.Cart.SuppierId == SuppierId).ToList();

                if (cartDetails != null)
                {
                    foreach (var item in cartDetails)
                    {
                        Product product = db.Product.Where(m => m.ProductId == item.ProductId).FirstOrDefault();
                        CartDetailModel cartDetailModel = new CartDetailModel()
                        {
                            CartDetailId = item.CartDetailId,
                            CartId = (int)item.CartId,
                            ProductId = (int)item.ProductId,
                            ProductName = product.Name,
                            Price = (int)product.Price,
                            Quantity = (int)item.Quantity,
                            Total = (int)product.Price
                        };
                        list.Add(cartDetailModel);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<ProductModel> GetProductList(int SuppierId)
        {
            try
            {
                List<ProductModel> list = new List<ProductModel>();
                List<Product> products = db.Product.ToList();
                list = AdminHelper.ConvertProductListToList(products);
                foreach (var product in list)
                {
                    CartDetail cartDetail = db.CartDetail.Where(m => m.Cart.SuppierId == SuppierId && m.ProductId == product.ProductId).FirstOrDefault();
                    if (cartDetail != null)
                    {
                        product.IsAddCart = (bool)cartDetail.IsAdd;
                    }
                    else
                    {
                        product.IsAddCart = false;
                    }
                }
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool IncreaseQuantity(int CartDetailId)
        {
            try
            {
                int increase = 0;
                CartDetail cartDetail = db.CartDetail.Where(m => m.CartDetailId == CartDetailId).FirstOrDefault();
                Product product = db.Product.Where(m => m.ProductId == cartDetail.ProductId).FirstOrDefault();
                if (cartDetail != null && product.Quantity > 0)
                {
                    cartDetail.Quantity += 1;
                    increase = db.SaveChanges();
                }

                return increase > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool RemoveCart(int CartDetailId, int SuppierId)
        {
            try
            {
                int RemoveCart = 0;
                Cart cart = db.Cart.Where(m => m.SuppierId == SuppierId).FirstOrDefault();
                if (cart != null)
                {
                    CartDetail cartDetail = db.CartDetail.Where(m => m.CartDetailId == CartDetailId).FirstOrDefault();
                    cartDetail.IsAdd = false;
                    db.CartDetail.Remove(cartDetail);
                    RemoveCart = db.SaveChanges();
                }

                return RemoveCart > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<OrderModel> OrderList(int SuppierId)
        {
            try
            {
                List<Orders> orders = db.Orders.Where(m => m.SuppierId == SuppierId).ToList();
                foreach (var item in orders)
                {
                    List<OrderDetail> orderDetails = item.OrderDetail.ToList();
                }
                List<OrderModel> orderModelList = OrderHelper.ConvertOrderListToList(orders);



                return orderModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

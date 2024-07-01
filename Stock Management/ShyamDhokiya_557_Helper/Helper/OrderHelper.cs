using ShyamDhokiya_557_Model.DbContext;
using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Helper.Helper
{
    public class OrderHelper
    {
        public static List<OrderModel> ConvertOrderListToList(List<Orders> orders)
        {
            try
            {
                List<OrderModel> orderModelList = new List<OrderModel>();
                if(orders != null)
                {
                    foreach(var item in orders)
                    {
                        OrderModel orderModel = new OrderModel();
                        orderModel.OrderId = item.OrderId;
                        orderModel.SuppierId = (int)item.SuppierId;
                        orderModel.SuppierName = item.Suppiers.Name;
                        orderModel.SuppierEmail = item.Suppiers.Email;
                        orderModel.OrderTotal = (int)item.OrderTotal;
                        orderModel.OrderDate = (DateTime)item.OrderDate;
                        List<OrderDetail> orderDetails = item.OrderDetail.ToList();
                        orderModel.OrderDetailList = ConvertOrderDetailListToList(orderDetails);
                        orderModelList.Add(orderModel);
                    }

                }

                return orderModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<OrderDetailModel> ConvertOrderDetailListToList(List<OrderDetail> orderDetails)
        {
            try
            {
                List<OrderDetailModel> orderDetailModelList = new List<OrderDetailModel>();
                if (orderDetails != null)
                {
                    foreach (var item in orderDetails)
                    {
                        OrderDetailModel orderDetailModel = new OrderDetailModel();
                        orderDetailModel.OrderDetailId = item.OrderDetailId;
                        orderDetailModel.OrderId = (int)item.OrderId;
                        orderDetailModel.ProductId = (int)item.ProductId;
                        orderDetailModel.Quantity = (int)item.Quantity;
                        orderDetailModel.ProductName = item.Product.Name;
                        orderDetailModel.TotalPrice = (int)item.Price;
                        orderDetailModel.ProductPrice = (int)item.Product.Price;
                        orderDetailModelList.Add(orderDetailModel);
                    }
                }

                return orderDetailModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

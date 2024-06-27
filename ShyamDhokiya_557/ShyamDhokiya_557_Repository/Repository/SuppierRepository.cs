using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Repository.Repository
{
    public interface SuppierRepository
    {
        List<ProductModel> GetProductList(int SuppierId);
        List<CartDetailModel> GetCartItemList(int SuppierId);
        bool AddCart(int ProductId,int SuppierId);
        bool RemoveCart(int CartDetailId, int SuppierId);
        bool AddOrder(int SuppierId);
        bool IncreaseQuantity(int CartDetailId);
        bool DecreaseQuantity(int CartDetailId);
        List<OrderModel> OrderList(int SuppierId);
        
    }
}

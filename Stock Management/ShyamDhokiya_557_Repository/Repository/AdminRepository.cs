using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Repository.Repository
{
    public interface AdminRepository
    {
        bool Addproduct(ProductModel productModel);
        bool EditProduct(ProductModel productModel);
        ProductModel GetProductById(int ProductId);
        List<UserModel> GetSuppierList();
        List<ProductModel> GetProductList();
        bool DeleteProduct(int ProductId);
        List<OrderTableModel> OrderList();
        List<OrderTableModel> FilterData(DateTime StartDate, DateTime EndDate, int ProductName);
    }
}

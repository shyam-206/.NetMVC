using MVCCrud_Model.Context;
using MVCCrud_Model.CustomModel;
using System.Collections.Generic;

namespace MVCCrud_Repository.Interface
{
    public interface IProduct
    {
        List<ProductModel> GetProductList();

        Product AddProduct(ProductModel productModel);

        ProductModel GetProductById(int? id);

        bool DeleteProduct(int? id);

        bool EditProduct(ProductModel productModel);

    }
}

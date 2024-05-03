using MVCCrud_Helper.Helper;
using MVCCrud_Model.Context;
using MVCCrud_Model.CustomModel;
using MVCCrud_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVCCrud_Repository.Service
{
    public class ProductService : IProduct
    {

        private readonly shyamEntities _context;

        public ProductService()
        {
            _context = new shyamEntities();
        }

        public List<ProductModel> GetProductList()
        {
            try
            {
                
                List<Product> productList = _context.Product.ToList();
                List<ProductModel> productModelList = ProductHelper.GetProductByHelper(productList);
                return productModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Product AddProduct(ProductModel productModel)
        {
            try
            {
                if (productModel != null)
                {
                    Product _product = ProductHelper.ConvertProductModelToProduct(productModel);
                    _context.Product.Add(_product);
                    _context.SaveChanges();
                    return _product;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }

        public bool DeleteProduct(int? id)
        {
            try
            {
                int isSuccess = 0;
                Product _product = _context.Product.Find(id);

                if (_product != null)
                {
                    _context.Product.Remove(_product);
                    isSuccess = _context.SaveChanges();
                }
                if (isSuccess > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public ProductModel GetProductById(int? id)
        {
            try
            {
                Product product = _context.Product.Find(id);
                ProductModel productModel = ProductHelper.ConvertProductToProductModel(product);
                return productModel;
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
                int _isEdited = 0;
                Product product = ProductHelper.ConvertProductModelToProduct(productModel);
                _context.Entry(product).State = EntityState.Modified;
                _isEdited = _context.SaveChanges();

                if (_isEdited > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

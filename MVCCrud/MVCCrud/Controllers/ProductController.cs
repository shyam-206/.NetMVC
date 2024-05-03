using MVCCrud.Authorization;
using MVCCrud_Model.Context;
using MVCCrud_Model.CustomModel;
using MVCCrud_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCCrud.Controllers
{
    [CustomActionFilter]
    public class ProductController : Controller
    {

        private readonly IProduct _repo;

        public ProductController(IProduct repo)
        {
            this._repo = repo;
        }

        public ActionResult Index()
        {
            try
            {
                List<ProductModel> productList = _repo.GetProductList();
                return View(productList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Add(ProductModel prodcutModel)
        {
            try
            {
                if (prodcutModel != null && ModelState.IsValid)
                {
                    
                    Product _product = _repo.AddProduct(prodcutModel);
                    ModelState.Clear();
                    TempData["AddProdcut"] = "New Product added";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(int? productId)
        {
            try
            {
                ProductModel productModel = new ProductModel();
                if (productId.HasValue && productId > 0)
                {
                    productModel = _repo.GetProductById(productId);
                    return View(productModel);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            try
            {

                if (productModel != null && ModelState.IsValid)
                {
                    bool checkingEdit = _repo.EditProduct(productModel);

                    if (checkingEdit)
                    {
                        TempData["edit"] = "Product Edit Successfully";
                        Console.WriteLine("Edit Suceessfully");
                    }
                    else
                    {
                        Console.WriteLine("Not Edited");
                    }
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult Delete(int? productId)
        {
            try
            {
                if (productId.HasValue && productId > 0)
                {
                    bool checkingDelete = _repo.DeleteProduct(productId);

                    if (checkingDelete)
                    {
                        TempData["delete"] = "Product delete SuccessFully";
                        Console.WriteLine("Deleted Suceessfully");
                    }
                    else
                    {
                        TempData["delete1"] = "Something went Wrong";
                        Console.WriteLine("Not deleted ");
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
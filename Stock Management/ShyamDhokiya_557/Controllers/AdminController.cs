using Newtonsoft.Json;
using ShyamDhokiya_557.Common;
using ShyamDhokiya_557.CustomFilter;
using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShyamDhokiya_557.Controllers
{
    [CustomAuthencation]
    [CustomAuthorizationAdmin]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Product()
        {
            try
            {
                string url = "api/AdminAPI/GetProductList";
                string res = await WebHelper.HttpRequestResponse(url);
                List<ProductModel> list = JsonConvert.DeserializeObject<List<ProductModel>>(res);
                return View(list);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult Addproduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Addproduct(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                String url = "api/AdminAPI/Addproduct";
                string content = JsonConvert.SerializeObject(productModel);
                string res = await WebHelper.HttpRequestResponsePost(url, content);

                bool save = JsonConvert.DeserializeObject<bool>(res);
                if (save)
                {
                    TempData["Add"] = "Add Product Successfully";
                    return RedirectToAction("Product");
                }
                else
                {
                    TempData["AddError"] = "Add Successfully";
                    return View(productModel);
                }
            }
            else
            {
                TempData["AddData"] = "Please Required all Fields";
                return View(productModel);
            }
        }

        public async Task<ActionResult> SuppierList()
        {
            try
            {
                string url = "api/AdminAPI/GetSuppierList";
                string res = await WebHelper.HttpRequestResponse(url);
                List<UserModel> list = JsonConvert.DeserializeObject<List<UserModel>>(res);
                return View(list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditProduct(int ProductId)
        {
            try
            {
                string url = $"api/AdminAPI/GetProductById?ProductId={ProductId}";
                string res = await WebHelper.HttpRequestResponse(url);
                ProductModel productModel = JsonConvert.DeserializeObject<ProductModel>(res);
                return View(productModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> EditProduct(ProductModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string url = "api/AdminAPI/EditProduct";
                    string content = JsonConvert.SerializeObject(productModel);
                    string res = await WebHelper.HttpRequestResponsePost(url, content);
                    bool saveEdit = JsonConvert.DeserializeObject<bool>(res);

                    if (saveEdit)
                    {
                        TempData["Edit"] = "Edit Successfully";
                        return RedirectToAction("Product");
                    }
                    else
                    {
                        TempData["EditError"] = "Something went Wrong";
                        return View(productModel);
                    }
                }
                else
                {
                    TempData["EditData"] = "Please Required all Fields";
                    return View(productModel);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> Deleteproduct(int ProductId)
        {
            try
            {
                string url = $"api/AdminAPI/DeleteProduct?ProductId={ProductId}";
                string res = await WebHelper.HttpRequestResponse(url);
                bool delete = JsonConvert.DeserializeObject<bool>(res);

                if (delete)
                {
                    TempData["delete"] = "delete Product Successfully";
                    return RedirectToAction("Product");
                }
                else
                {
                    TempData["DeleteError"] = "Something went to Wrong";
                    return RedirectToAction("Product");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> OrderList()
        {
            try
            {
                string url = "api/AdminAPI/OrderList";
                string res = await WebHelper.HttpRequestResponse(url);
                List<OrderTableModel> orderList = JsonConvert.DeserializeObject<List<OrderTableModel>>(res);

                string url1 = "api/AdminAPI/GetProductList";
                string res1 = await WebHelper.HttpRequestResponse(url1);
                ViewBag.Product = JsonConvert.DeserializeObject<List<ProductModel>>(res1);
                return View(orderList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
         
        [HttpPost]
        public async Task<ActionResult> OrderList(DateTime StartDate, DateTime EndDate, int ProductName)
        {
            try
            {
                if (StartDate > EndDate)
                {
                    TempData["Error"] = "StartDate can not be greater than EndDate";
                    return View();
                }
                else
                {
                    string url = $"api/AdminAPI/FilterData?StartDate={StartDate}&EndDate={EndDate}&ProductName={ProductName}";
                    string res = await WebHelper.HttpRequestResponse(url);
                    List<OrderTableModel> list = JsonConvert.DeserializeObject<List<OrderTableModel>>(res);
                    string url1 = "api/AdminAPI/GetProductList";
                    string res1 = await WebHelper.HttpRequestResponse(url1);
                    ViewBag.Product = JsonConvert.DeserializeObject<List<ProductModel>>(res1);

                    ViewBag.StartDate = StartDate.ToString("yyyy-MM-dd");
                    ViewBag.EndDate = EndDate.ToString("yyyy-MM-dd");
                    ViewBag.SelectedProduct = ProductName;
                    return View(list);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
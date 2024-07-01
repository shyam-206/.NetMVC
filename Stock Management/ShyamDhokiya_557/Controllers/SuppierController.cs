using Newtonsoft.Json;
using ShyamDhokiya_557.Common;
using ShyamDhokiya_557.CustomFilter;
using ShyamDhokiya_557.Session;
using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShyamDhokiya_557.Controllers
{
    [CustomAuthencation]
    [CustomAuthorizationSuppier]
    public class SuppierController : Controller
    {
        // GET: Suppier
        public async Task<ActionResult> Index()
        {           
            try
            {
                string url = $"api/SuppierAPI/GetProductList?SuppierId={SessionHelper.UserId}";
                string res = await WebHelper.HttpRequestResponse(url);
                List<ProductModel> list = JsonConvert.DeserializeObject<List<ProductModel>>(res);
                return View(list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<ActionResult> AddCart(int ProductId)
        {
            try
            {
                int SuppierId = SessionHelper.UserId;
                string url = $"api/SuppierAPI/AddCart?ProductId={ProductId}&SuppierId={SuppierId}";
                string res = await WebHelper.HttpRequestResponse(url);
                bool SaveCart = JsonConvert.DeserializeObject<bool>(res);

                if (SaveCart)
                {
                    TempData["AddCart"] = "Cart Add Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorCart"] = "Something is wrong to Add cart";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public async Task<ActionResult> ViewCart()
        {
            try
            {
                int SuppierId = SessionHelper.UserId;
                string url = $"api/SuppierAPI/GetCartItemList?SuppierId={SuppierId}";
                string res = await WebHelper.HttpRequestResponse(url);
                List<CartDetailModel> list = JsonConvert.DeserializeObject<List<CartDetailModel>>(res);
                return View(list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> IncreaseQuantity(int CartDetailId)
        {
            try
            {
                string url = $"api/SuppierAPI/IncreaseQuantity?CartDetailId={CartDetailId}";
                string res = await WebHelper.HttpRequestResponse(url);
                bool ans = JsonConvert.DeserializeObject<bool>(res);
                if (ans)
                {
                    TempData["Done"] = "Quantity Increase";
                }
                return RedirectToAction("ViewCart");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<ActionResult> DecreaseQuantity(int CartDetailId)
        {
            try
            {
                string url = $"api/SuppierAPI/DecreaseQuantity?CartDetailId={CartDetailId}";
                string res = await WebHelper.HttpRequestResponse(url);
                bool ans = JsonConvert.DeserializeObject<bool>(res);
                if (ans)
                {
                    TempData["Done"] = "Quantity Decrease";
                }
                return RedirectToAction("ViewCart");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> RemoveCart(int CartDetailId)
        {
            try
            {
                int SuppierId = SessionHelper.UserId;
                string url = $"api/SuppierAPI/RemoveCart?CartDetailId={CartDetailId}&SuppierId={SuppierId}";
                string res = await WebHelper.HttpRequestResponse(url);
                bool SaveCart = JsonConvert.DeserializeObject<bool>(res);

                if (SaveCart)
                {
                    TempData["RemoveCart"] = "Cart Remove Successfully";      
                }
                else
                {
                    TempData["ErrorCart"] = "Something is wrong to Add cart";
                }
                return RedirectToAction("ViewCart");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<ActionResult> AddOrder()
        {
            try
            {
                int SuppierId = SessionHelper.UserId;
                string url = $"api/SuppierAPI/AddOrder?SuppierId={SuppierId}";
                string res = await WebHelper.HttpRequestResponse(url);
                bool AddOrder = JsonConvert.DeserializeObject<bool>(res);

                if (AddOrder)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ViewCart");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> OrderDetail()
        {
            try
            {
                int SuppierId = SessionHelper.UserId;
                string url = $"api/SuppierAPI/OrderList?SuppierId={SuppierId}";
                string res = await WebHelper.HttpRequestResponse(url);
                List<OrderModel> orderModelList = JsonConvert.DeserializeObject<List<OrderModel>>(res);
                return View(orderModelList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
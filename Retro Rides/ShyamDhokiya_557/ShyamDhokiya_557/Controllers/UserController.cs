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
    public class UserController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string res = await WebHelper.HttpRequestResponse($"api/User/BikeList?UserId={SessionHelper.UserId}");
            List<BikeModel> list = new List<BikeModel>();
            list = JsonConvert.DeserializeObject<List<BikeModel>>(res);
            foreach (var item in list)
            {
                item.BikeImages = item.Image.Split(',');
            }
            if (list != null && list.Count() > 0)
            {
                List<string> BikeBrandList = list.Select(m => m.Brand).ToList();
                ViewBag.BikeBrandList = BikeBrandList;
            }
            return View(list);
        }

        [HttpPost]
        public async Task<ActionResult> Index(FilterDataModel filterData)
        {
            if (filterData.LowestKMDriven == 0 || filterData.LowestKMDriven < 0)
            {
                filterData.LowestKMDriven = 0;
                ViewBag.LowestKMDriven = 0;
            }
            string res = await WebHelper.HttpRequestResponse($"api/User/BikeList?UserId={SessionHelper.UserId}");
            List<BikeModel> list = new List<BikeModel>();
            list = JsonConvert.DeserializeObject<List<BikeModel>>(res);
            foreach (var item in list)
            {
                item.BikeImages = item.Image.Split(',');
            }

            if (list != null && list.Count() > 0)
            {
                List<string> BikeBrandList = list.Select(m => m.Brand).ToList();
                ViewBag.BikeBrandList = BikeBrandList;
            }

            if (filterData.BrandName != " ")
            {
                list = list.Where(m => m.Brand == filterData.BrandName).ToList();
                ViewBag.BrandName = filterData.BrandName;
            }
            if (filterData.LowestKMDriven >= 0)
            {
                ViewBag.LowestKMDriven = filterData.LowestKMDriven;
                list = list.Where(m => m.kilometresDriven >= filterData.LowestKMDriven).ToList();
            }

            if (filterData.HighestKMDriven != 0)
            {
                ViewBag.HighestKMDriven = filterData.HighestKMDriven;
                list = list.Where(m => m.kilometresDriven <= filterData.HighestKMDriven).ToList();
            }
            return View(list);
        }

        public async Task<ActionResult> GetMyFavouriteBikes()
        {
            string res = await WebHelper.HttpRequestResponse($"api/User/BikeList?UserId={SessionHelper.UserId}");
            List<BikeModel> list = new List<BikeModel>();
            list = JsonConvert.DeserializeObject<List<BikeModel>>(res);
            foreach (var item in list)
            {
                item.BikeImages = item.Image.Split(',');
            }
            List<BikeModel> newList = new List<BikeModel>();

            foreach (var item in list)
            {
                if (item.IsFavourite == true)
                {
                    newList.Add(item);
                }
            }

            return View(newList);
        }

        public async Task<ActionResult> GetBike(int BikeId)
        {
            BikeModel bikeModel = new BikeModel();
            string res = await WebHelper.HttpRequestResponse($"api/Main/GetBikeById?BikeId={BikeId}");
            bikeModel = JsonConvert.DeserializeObject<BikeModel>(res);
            bikeModel.BikeImages = bikeModel.Image.Split(',');
            return View(bikeModel);
        }

        [HttpPost]
        public async Task<ActionResult> ToggleFavourite(int BikeId)
        {

            int UserId = SessionHelper.UserId;
            string res = await WebHelper.HttpRequestResponse($"api/User/ToggleFavourite?BikeId={BikeId}&UserId={UserId}");
            var result = JsonConvert.DeserializeObject<FavoriteToggleResult>(res);

            if (result != null && result.Success == 1)
            {
                return Json(new { success = true, isFavorite = result.IsFavorite });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public async Task<ActionResult> ViewProfile(int UserId)
        {
            string res = await WebHelper.HttpRequestResponse($"api/User/GetUserProfile?UserId={UserId}");
            UserModel userModel = JsonConvert.DeserializeObject<UserModel>(res);

            string res1 = await WebHelper.HttpRequestResponse("api/User/GetCountryList");
            ViewBag.CountryList = JsonConvert.DeserializeObject<List<CountryModel>>(res1);
            return View(userModel);
        }

        private string GetUniqueImage(HttpPostedFileBase file)
        {
            string fileName = DateTime.Now.ToString("dd-MM-yyyy-ss") + file.FileName;
            file.SaveAs(HttpContext.Server.MapPath("~/Content/Proflies/") + fileName);
            return fileName;
        }

        [HttpPost]
        public async Task<ActionResult> ViewProfile(UserModel userModel)
        {
            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                if (userModel.Image != null)
                {
                    userModel.Image = GetUniqueImage(Request.Files[0]);
                }
                else
                {

                    userModel.Image = null;
                }
                string res = await WebHelper.HttpRequestResponsePost("api/User/EditUserProfile", JsonConvert.SerializeObject(userModel));
                bool edit = JsonConvert.DeserializeObject<bool>(res);
                if (edit)
                {
                    TempData["success"] = "Edit Profile Successfully";
                    Session["ProfileImage"] = userModel.Image;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(userModel);
                }
            }
            else
            {
                return View(userModel);
            }
        }
        public async Task<JsonResult> GetStateById(int CountryId)
        {
            string res = await WebHelper.HttpRequestResponse($"api/User/GetStateById?CountryId={CountryId}");
            List<StateModel> list = JsonConvert.DeserializeObject<List<StateModel>>(res);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCityById(int @StateId)
        {
            string res = await WebHelper.HttpRequestResponse($"api/User/GetCityById?StateId={StateId}");
            List<CityModel> list = JsonConvert.DeserializeObject<List<CityModel>>(res);
            return Json(list, JsonRequestBehavior.AllowGet);
        }



    }
}
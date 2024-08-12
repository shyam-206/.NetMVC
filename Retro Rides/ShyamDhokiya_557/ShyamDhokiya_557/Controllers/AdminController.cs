using Newtonsoft.Json;
using ShyamDhokiya_557.Common;
using ShyamDhokiya_557.CustomFilter;
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
    public class AdminController : Controller
    {
        // GET: Admin
        public async Task<ActionResult> Index()
        {
            string res = await WebHelper.HttpRequestResponse("api/Main/BikeList");
            List<BikeModel> list = new List<BikeModel>();
            list = JsonConvert.DeserializeObject<List<BikeModel>>(res);
            foreach (var item in list)
            {
                item.BikeImages = item.Image.Split(',');
            }
            return View(list);
        }

        public ActionResult AddBike()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddBike(BikeModel bikeModel)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0 || bikeModel.Image.EndsWith("JPG") || bikeModel.Image.EndsWith("PNG") || bikeModel.Image.EndsWith("JPEG"))
                {
                    string a = "";
                    for (var i = 0; i < Request.Files.Count; i++)
                    {
                        a += GetFileUniqueName(Request.Files[i]);
                        if (i < Request.Files.Count - 1)
                            a += ",";
                    }
                    bikeModel.Image = a;
                }


                string res = await WebHelper.HttpRequestResponsePost("api/Main/AddBike", JsonConvert.SerializeObject(bikeModel));
                bool save = JsonConvert.DeserializeObject<bool>(res);
                if (save)
                {
                    TempData["success"] = "Add Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(bikeModel);
                }
            }
            else
            {

                return View(bikeModel);
            }
        }

        public string GetFileUniqueName(HttpPostedFileBase file)
        {
            string fileName = DateTime.Now.ToString("dd-MM-yyyy-ss") + file.FileName;
            file.SaveAs(HttpContext.Server.MapPath("~/Content/BikeImage/") + fileName);
            return fileName;
        }

        public async Task<ActionResult> Edit(int BikeId)
        {
            string res = await WebHelper.HttpRequestResponse($"api/Main/GetBikeById?BikeId={BikeId}");
            BikeModel bikeModel = new BikeModel();
            bikeModel = JsonConvert.DeserializeObject<BikeModel>(res);
            bikeModel.BikeImages = bikeModel.Image.Split(',');
            return View(bikeModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(BikeModel bikeModel)
        {
            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0 && Request.Files.Count < 5 && bikeModel.Image != null)
                {
                    string a = "";
                    for (var i = 0; i < Request.Files.Count; i++)
                    {
                        a += GetFileUniqueName(Request.Files[i]);
                        if (i < Request.Files.Count - 1)
                            a += ",";
                    }
                    bikeModel.Image = a;
                }
                else
                {
                    bikeModel.Image = await GetImageByBikeId(bikeModel.BikeId);
                }


                string res = await WebHelper.HttpRequestResponsePost("api/Main/EditBike", JsonConvert.SerializeObject(bikeModel));
                bool save = JsonConvert.DeserializeObject<bool>(res);
                if (save)
                {
                    TempData["success"] = "Edit Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    
                     bikeModel.Image = await GetImageByBikeId(bikeModel.BikeId);
                    return View(bikeModel);
                }
            }
            else
            {

                bikeModel.Image = await GetImageByBikeId(bikeModel.BikeId);
                return View(bikeModel);
            }

        }

        public async Task<ActionResult> Delete(int BikeId)
        {
            string res = await WebHelper.HttpRequestResponse($"api/Main/DeleteBike?BikeId={BikeId}");
            bool save = JsonConvert.DeserializeObject<bool>(res);
            return RedirectToAction("Index");
        }

        public async Task<string> GetImageByBikeId(int BikeId)
        {
            string res = await WebHelper.HttpRequestResponse($"api/Main/GetImageByBikeId?BikeId={BikeId}");
            string Image = JsonConvert.DeserializeObject<string>(res);
            return Image;
        }
    }
}
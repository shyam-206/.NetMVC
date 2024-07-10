using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using ShyamDhokiya_557.Common;
using ShyamDhokiya_557.CustomFilter;
using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        public async Task<ActionResult> OrderList(DateTime? StartDate, DateTime? EndDate, int ProductName)
        {
            try
            {
                if (StartDate == null || EndDate == null)
                {
                    ViewBag.StartDate = StartDate?.ToString("yyyy-MM-dd");
                    ViewBag.EndDate = EndDate?.ToString("yyyy-MM-dd");
                    ViewBag.SelectedProduct = ProductName;
                    TempData["errorDate"] = "Please Select a Date";
                    return View();
                }
                else if (StartDate > EndDate)
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

                    ViewBag.StartDate = StartDate.Value.ToString("yyyy-MM-dd");
                    ViewBag.EndDate = EndDate.Value.ToString("yyyy-MM-dd");
                    ViewBag.SelectedProduct = ProductName;
                    return View(list);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> DownloadReport(DateTime? StartDate, DateTime? EndDate, int?  ProductName)
        {
            if(StartDate == null || EndDate == null || ProductName == null)
            {
                StartDate = DateTime.Parse("2024-01-01");
                EndDate = DateTime.Today;
                ProductName = 0;
            }
            string url = $"api/AdminAPI/FilterData?StartDate={StartDate}&EndDate={EndDate}&ProductName={ProductName}";
            string res = await WebHelper.HttpRequestResponse(url);
            List<OrderTableModel> list = JsonConvert.DeserializeObject<List<OrderTableModel>>(res);
            byte[] pdfData = GeneratePdf(list);

            // Return the PDF data as a File result
            return File(pdfData, "application/pdf", "Report.pdf");
        }

        [NonAction]
        private byte[] GeneratePdf(List<OrderTableModel> orders)
        {
            using (var output = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(PageSize.A4, 10, 10, 20, 20);
                PdfWriter.GetInstance(document, output);

                document.Open();
                // Define fonts for the table
                var fontHeader = FontFactory.GetFont("Arial", 12, Font.BOLD);
                var fontCell = FontFactory.GetFont("Arial", 10, Font.NORMAL);


                var table = new PdfPTable(6);
                table.WidthPercentage = 100; // Set table width to 100% of the page
                table.SetWidths(new float[] { 2f, 2f, 2f, 1f, 1f, 2f }); // Set relative column widths

                /*table.AddCell("Product Name");
                table.AddCell("Email");
                table.AddCell("Quantity");
                table.AddCell("Price");
                table.AddCell("Total");
                table.AddCell("Order Date");

                foreach (var order in orders)
                {
                    table.AddCell(order.ProductName.ToString());
                    table.AddCell(order.Email.ToString());
                    table.AddCell(order.Quantity.ToString());
                    table.AddCell(order.Price.ToString());
                    table.AddCell(order.TotalPrice.ToString());
                    table.AddCell(order.OrderDate.ToString().Split(' ')[0]);
                }*/

                AddCellToHeader(table, "Product Name", fontHeader);
                AddCellToHeader(table, "Email", fontHeader);
                AddCellToHeader(table, "Quantity", fontHeader);
                AddCellToHeader(table, "Price", fontHeader);
                AddCellToHeader(table, "Total", fontHeader);
                AddCellToHeader(table, "Order Date", fontHeader);

                // Iterate over the orders collection and add each order's details to the table
                foreach (var order in orders)
                {
                    AddCellToBody(table, order.ProductName.ToString(), fontCell);
                    AddCellToBody(table, order.Email.ToString(), fontCell);
                    AddCellToBody(table, order.Quantity.ToString(), fontCell);
                    AddCellToBody(table, order.Price.ToString(), fontCell);
                    AddCellToBody(table, order.TotalPrice.ToString(), fontCell);
                    AddCellToBody(table, order.OrderDate.ToString("yyyy-MM-dd"), fontCell);
                }


                document.Add(table);
                document.Close(); // Finalize the document
                return output.ToArray(); // Return the PDF data as a byte array
            }
        }

        [NonAction]
        private void AddCellToHeader(PdfPTable table, string text, Font font)
        {
            var cell = new PdfPCell(new Phrase(text, font))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 5,
                BackgroundColor = BaseColor.LIGHT_GRAY
            };
            table.AddCell(cell);
        }

        [NonAction]
        private void AddCellToBody(PdfPTable table, string text, Font font)
        {
            var cell = new PdfPCell(new Phrase(text, font))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 5
            };
            table.AddCell(cell);
        }
    }
}
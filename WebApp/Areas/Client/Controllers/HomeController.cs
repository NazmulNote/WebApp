using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Models;
using WebApp.Areas.Client.Data;
using WebApp.Areas.Client.Models;

namespace WebApp.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly HomeData _homeData;
        private readonly ProductViewData _productViewData;
        private readonly CustomerData _customerData;
        public HomeController()
        {
            _homeData = new HomeData();
            _productViewData = new ProductViewData();
            _customerData = new CustomerData();
        }
        [HttpGet]
        public IActionResult Index()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                viewModel.SliderList = _homeData.GetSliderList();
                viewModel.SiteInfo = _homeData.GetSiteInfo();
                viewModel.CatList = _productViewData.GetCategoryList();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Contact()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                viewModel.CompanyInfo = _homeData.GetCompanyInfo();
                viewModel.SiteInfo = _homeData.GetSiteInfo();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult About()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                viewModel.CompanyInfo = _homeData.GetCompanyInfo();
                viewModel.AboutPage = _homeData.GetAboutPageInfo();
                viewModel.TeamMemberList = _homeData.GetTeamMemberList();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Client()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                viewModel.ClientList = _homeData.GetClientList();
                viewModel.SiteInfo = _homeData.GetSiteInfo();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Service()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                viewModel.ServiceList = _homeData.GetServiceList(null);
                viewModel.SiteInfo = _homeData.GetSiteInfo();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult ServiceDetails(int? ID)
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                viewModel.Service = _homeData.GetService(ID);
                viewModel.ServiceList = _homeData.GetServiceList(viewModel.Service.ServiceCatId).Where(x=>x.ID !=ID).ToList();
                viewModel.SiteInfo = _homeData.GetSiteInfo();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        #region -----------------------------------------------Product Start
        [HttpGet]
        public IActionResult Product(int? catId)
        {
            ClientViewModel viewModel = new ClientViewModel();
            viewModel.SubCatList = new List<SubCatMDL>();
            try
            {
                viewModel.SiteInfo = _homeData.GetSiteInfo();
                viewModel.CatList = _productViewData.GetCategoryList();
                var subCats = _productViewData.GetSubCatList(catId);
                foreach(var subCat in subCats)
                {
                    var subCatMDL = new SubCatMDL()
                    {
                        ID = subCat.ID,
                        Name = subCat.SubCatName,
                        SubChildCatList = _productViewData.GetSubChildCatList(subCat.ID)
                        .Select(x => new SubChildCatMDL()
                        {
                            ID = x.ID,
                            Name = x.SubChildCatName,
                        }).ToList()
                    };
                    viewModel.SubCatList?.Add(subCatMDL);
                }
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult ProductView(int?catId,int?subcatId,int?childId)
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                if(catId != null && subcatId == 0 && childId == 0)
                {
                    viewModel.ProductList = _productViewData.GetProductList("Cat",catId, null, null);
                    return PartialView("_ProductView", viewModel);
                }
                else if(subcatId !=0 && childId !=0)  
                {
                    viewModel.ProductList = _productViewData.GetProductList("ChildCat", null, null, childId);
                    return PartialView("_ProductView", viewModel);
                } else if (subcatId !=null && childId == 0)
                {
                    viewModel.ProductList = _productViewData.GetProductList("SubCat", null, subcatId,null);
                    return PartialView("_ProductView", viewModel);
                }
                
            }catch (Exception ex) { }
            return PartialView("_ProductView", viewModel);
        }
        [HttpGet]
        public IActionResult ProductDetails(int ID)
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                viewModel.SiteInfo = _homeData.GetSiteInfo();
                viewModel.Product = _productViewData.GetProduct(ID);
                viewModel.ProductList = _productViewData.GetProductList("ChildCat", null, null, viewModel.Product.SubChildCatId).Where(x =>x.ID != viewModel.Product.ID).ToList();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Cart()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                GetCourierChargeList();
                viewModel.CatList = _productViewData.GetCategoryList();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public void GetCourierChargeList()
        {
            try
            {
                var courierCharge = _customerData.GetCourierChargeList();
                var courierChargeList = courierCharge.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Location,
                }).ToList();
                ViewBag.CourierCharge = courierChargeList;
            }catch(Exception ex) { }
            
        }
        public JsonResult GetCourierChargeAmountDay(int ID)
        {
            try
            {
                var courierCharge = _customerData.GetCourierCharge(ID);
                return Json(courierCharge);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error fetching courier charge." });
            }
        }

        #endregion --------------------------------------------Product End
        [HttpPost]
        public IActionResult MessageSet(ClientViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.Message != null)
                {
                    MessageMDL message = new MessageMDL();

                    if (viewModel.Message.ID == 0)
                    {
                        // Insert
                        message.Type = "Message";
                        message.Name = viewModel.Message.Name;
                        message.Email = viewModel.Message.Email;
                        message.Subject = viewModel.Message.Subject;
                        message.Body = viewModel.Message.Body;
                        message.InsertId = 0;

                        var result = _homeData.MessageInsertUpdate(message, "Insert");
                        return Json(result.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

            return Json(0);
        }
        [HttpPost]
        public IActionResult SubscribeSet(string? Email)
        {
            try
            {
                if (Email != null)
                {
                    MessageMDL message = new MessageMDL();
                    message.Type = "Subscribe";
                    message.Email = Email;
                    message.InsertId = 0;
                    var result = _homeData.MessageInsertUpdate(message, "Insert");
                    return Json(result.ID);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

            return Json(0);
        }
    }
}

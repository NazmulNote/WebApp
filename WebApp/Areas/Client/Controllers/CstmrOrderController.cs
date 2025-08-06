using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Models;
using WebApp.Areas.Client.Data;
using WebApp.Areas.Client.Models;

namespace WebApp.Areas.Client.Controllers
{
    [Area("Client")]
    public class CstmrOrderController : Controller
    {
        private readonly OrderProductData _orderProductData;
        public CstmrOrderController()
        {
            _orderProductData = new OrderProductData();
        }
        [HttpPost]
        public IActionResult Submit([FromBody] OrderProductRequestMDL orderRequest)
        {
            if (HttpContext.Session.GetString("CustomerID") == null || HttpContext.Session.GetString("CustomerID") == "") { return RedirectToAction("SignIn", "Cstmr"); }
            try
            {
                if (orderRequest != null)
                {
                    var customerId = HttpContext.Session.GetString("CustomerID");
                    var cartItems = orderRequest.Cart;

                    var maxOrderNo = _orderProductData.GetMaxOrderNo();
                    var orderNo = maxOrderNo.OrderNo + 1;

                    OrderProductMDL orderProduct = new OrderProductMDL();
                    OrderProductItemMDL productItem = new OrderProductItemMDL();

                    orderProduct.OrderNo = orderNo;
                    orderProduct.CustomerId = Convert.ToInt32(customerId);
                    orderProduct.TotalAmount = orderRequest.GrandTotal;
                    orderProduct.DeliveryAddress = orderRequest.DeliveryAddress;
                    orderProduct.CourierChargeId = orderRequest.CourierChargeId;
                    orderProduct.IsActive = true;
                    var result = _orderProductData.OrderProductSetUpdate(orderProduct, "Insert");
                    if (result != null) 
                    {
                        foreach(var cart in cartItems)
                        {
                            productItem.OrderProductId = result.ID;
                            productItem.ProductId = cart.Id;
                            productItem.Quantity = cart.Qty;
                            productItem.UnitPrice = cart.Price;
                            productItem.IsActive = true;
                            var resultItem = _orderProductData.OrderProductItemSetUpdate(productItem, "Insert");
                        }    
                    }
                    return Json(new { success = true, message = "Order submitted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid order data." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

    }
}

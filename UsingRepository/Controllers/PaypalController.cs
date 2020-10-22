using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UsingRepository.Configuration;
using UsingRepository.Core.ViewModels;

namespace UsingRepository.Controllers
{
    [Authorize]
    public class PaypalController : Controller
    {

        private Payment payment;
        private Payment CreatePayment(APIContext apicontext, string redirectUrl)
        {
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };

            var cart = Session["Cart"] as List<CartViewModel>;
            foreach (var model in cart)
            {
                itemList.items.Add(new Item()
                {
                    name = model.Name,
                    currency = "USD",
                    price = model.Price.ToString(),
                    quantity = model.Quantity.ToString(),
                    sku = "sku"
                });
            }

            var payer = new Payer()
            {
                payment_method = "paypal"
            };

            var redirectUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&cancel=true",
                return_url = redirectUrl
            };

            //adding taxes,shipping and subtotal
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = cart.Sum(c => c.Price * c.Quantity).ToString()
            };

            //final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                details = details,
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString()
            };

            var transaction = new List<Transaction>();

            transaction.Add(new Transaction()
            {
                description = "Custom Description",
                amount = amount,
                item_list = itemList,
                invoice_number = Convert.ToString(new Random().Next(100000))
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transaction,
                redirect_urls = redirectUrls
            };
            return this.payment.Create(apicontext);
        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        public ActionResult PaymentWithPaypal(string cancel = null)
        {
            var apiContext = PaypalConfiguration.GetApiContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    var baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "Paypal/PaymentWithPaypal?";

                    var guid = Convert.ToString(new Random().Next(100000));

                    var createdPayment = CreatePayment(apiContext, baseUrl + "guid=" + guid);

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        var link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);

                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("failed");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return View("success");
        }

    }
}
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class PaymentsController : Controller
    {
        private AptEmsContext db = new AptEmsContext();

        [HttpGet]
        public ActionResult Payment(int agreementId)
        {
            // Retrieve client details for the given AgreementID using the stored procedure
            var clientDetails = db.Database.SqlQuery<PaymentDetailsViewModel>(
                "EXEC GetClientDetailsForPayment @AgreementID = {0}", agreementId).FirstOrDefault();

            if (clientDetails != null)
            {
                // Populate the ViewBag with payment method options
                ViewBag.PaymentMethods = new SelectList(new[]
                {
                    new SelectListItem { Text = "Credit Card", Value = "CreditCard" },
                    new SelectListItem { Text = "Debit Card", Value = "DebitCard" },
                    new SelectListItem { Text = "Net Banking", Value = "NetBanking" },
                    new SelectListItem { Text = "UPI", Value = "UPI" },
                    new SelectListItem { Text = "Wallets", Value = "Wallets" }
                }, "Value", "Text");

                // Pass the client details to the view
                return View(clientDetails);
            }

            // If no client details are found, return a NotFound error
            return HttpNotFound("Agreement not found.");
        }

        [HttpPost]
        public ActionResult ProcessPayment(int agreementId, decimal amount, string paymentMethod)
        {
            // Log the values to ensure they are being passed correctly
            Console.WriteLine($"Agreement ID: {agreementId}, Amount: {amount}, Payment Method: {paymentMethod}");

            // Step 1: Save payment details in the database
            var payment = new Payment
            {
                AgreementID = agreementId,
                AmountPaid = amount,
                PaymentMethod = paymentMethod,
                PaymentStatus = "Success", // assuming payment status after success
                PaymentDate = DateTime.Now,
                IsPaid = true // Mark payment as paid when this action is triggered
            };

            try
            {
                // Add payment to the Payments table and save changes
                db.Payments.Add(payment);
                db.SaveChanges();
                Console.WriteLine("Payment successfully saved.");

                // Step 2: Update ClientAgreements table
                var agreement = db.ClientAgreements.FirstOrDefault(a => a.AgreementID == agreementId);
                if (agreement != null)
                {
                    // Mark the agreement as paid
                    agreement.IsPaid = true;
                    db.Entry(agreement).State = EntityState.Modified; // Mark the agreement as modified
                    db.SaveChanges(); // Save the changes to the ClientAgreements table
                    Console.WriteLine("ClientAgreement 'IsPaid' updated to true.");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Log the error or output the error message
                        Console.WriteLine($"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                    }
                }
                // Handle the exception (you can return an error view or message)
                return View("Error");
            }

            // Step 3: Return a view that triggers JavaScript to open Razorpay in a new tab
            ViewBag.RazorpayUrl = "https://pages.razorpay.com/pl_HXd33LMenKYL2Q/view";
            return View("RedirectToRazorpay");
        }






        public ActionResult GenerateInvoice(int agreementId)
        {
            // Fetch client and payment details using your stored procedure
            var clientDetails = db.Database.SqlQuery<PaymentDetailsViewModel>(
                "EXEC GetClientDetailsForPayment @AgreementID = {0}", agreementId).FirstOrDefault();

            if (clientDetails != null)
            {
                // Add payment done date to the model
                clientDetails.PaymentDoneDate = DateTime.Now;

                // Fetch the payment details for the invoice
                var paymentDetails = db.Payments.Where(p => p.AgreementID == agreementId).FirstOrDefault();

                if (paymentDetails != null)
                {
                    // Add payment-related information to the model
                    clientDetails.AmountPaid = paymentDetails.AmountPaid;
                    clientDetails.PaymentMethod = paymentDetails.PaymentMethod;
                    clientDetails.PaymentStatus = paymentDetails.PaymentStatus;

                    return View(clientDetails);
                }
                else
                {
                    return HttpNotFound("Payment details not found.");
                }
            }

            return HttpNotFound("Client not found for invoice generation.");
        }

    }
}

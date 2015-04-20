using System;
using System.Web.Mvc;
using BusinessControllers;
using MVCCashMachine.Models;

namespace MVCCashMachine.Controllers
{
    public class HomeController : Controller
    {
        private BusinessController BusinessController //TODO - add support of the Unity to the project
        {
            get
            {
                return BusinessController.Instance;
            }
        }

        public ViewResult LoginCard()
        {
            return View("LoginCardView");
        }

        public ViewResult CheckCardNumber(CardModel model) //TODO: add server side validation
        {
            var isValid = BusinessController.CheckCardNumber(model.CardNumber);
            var retVal = isValid ? View("CheckPinView", new PinModel { CardNumber = model.CardNumber }) : View("LoginCardView");
            return retVal;
        }

        public ActionResult CheckPin(PinModel model)
        {
            var isValid = BusinessController.CheckPinNumber(model.CardNumber, model.Pin);
            if (isValid)
            {
                Session["CardNumber"] = model.CardNumber;
                var redirect = RedirectToAction("GetOperations", "Home");
                return redirect;
            }
            return View("CheckPinView", new PinModel { CardNumber = model.CardNumber, Pin = model.Pin });
        }

        public ViewResult GetOperations()
        {
            return View("OperationsView");
        }

        public ActionResult ShowBalance()
        {
            return View("BalanceView");
        }

        public ActionResult ShowWithdrawal()
        {
            return View("WithdrawalView");
        }

        public ActionResult WithdrawMoney()
        {
            return View("WithdrawalResultView");
        }
    }
}
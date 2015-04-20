using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BusinessControllers;
using MvcAtm.Models;
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
            Session["NumberOfTries"] = null;
            Session["CardNumber"] = null;
            return View("LoginCardView");
        }

        public ViewResult CheckCardNumber(CardModel model) //TODO: add server side validation
        {
            var isValid = BusinessController.CheckCardNumber(model.CardNumber);
            var retVal = isValid ? View("CheckPinView", new PinModel { CardNumber = model.CardNumber }) : View("LoginCardView");
            return retVal;
        }

        public ActionResult CheckPin(PinModel model) //TODO: need to refactor - too many returns
        {
            Session["CardNumber"] = null;
            var numberOfTriesObject = Session["NumberOfTries"];
            var numberOfTries = numberOfTriesObject != null ? (int) numberOfTriesObject : 0;
            var isValidResult = BusinessController.CheckPinNumber(model.CardNumber, model.Pin, numberOfTries);
            if (isValidResult.Successful)
            {
                Session["CardNumber"] = model.CardNumber;
                var redirect = RedirectToAction("GetOperations", "Home");
                return redirect;
            }
            if (!isValidResult.Blocked)
            {
                numberOfTries++;
                Session["NumberOfTries"] = numberOfTries;
            }
            else
            {
                return View("ErrorVIew", new ServerValidatedModel { ErrorModel = "Your card were blocked" });
            }

            return View("CheckPinView", new PinModel { CardNumber = model.CardNumber, Pin = model.Pin });
        }

        public ViewResult GetOperations()
        {
            return View("OperationsView");
        }

        public ActionResult ShowBalance()
        {
            var balance = BusinessController.GetCardBalance((long) Session["CardNumber"]);
            return View("BalanceView", new BalanceModel{Balance = balance});
        }

        public ActionResult ShowWithdrawal()
        {
            return View("WithdrawalView");
        }

        public ActionResult WithdrawMoney(WithdrawalModel model)
        {
            var cardNumber = (long) Session["CardNumber"];
            var withdrawalResult = BusinessController.WithdrawMoney(cardNumber,
                model.WithdrawalAmmount);
            if (withdrawalResult.Successfull)
            {
                return View("WithdrawalResultView", new WithdrawalResultModel
                {
                    Amount = model.WithdrawalAmmount,
                    Balance = withdrawalResult.Balance,
                    CardNumber = cardNumber,
                    DateTime = DateTime.Now
                });
            }
            return View("ErrorView", new ServerValidatedModel {ErrorModel = "Not enough money on account"});
        }
    }
}
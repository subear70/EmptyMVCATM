using System.Web.Mvc;
using BusinessControllers;
using MVCCashMachine.Models;

namespace MVCCashMachine.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private BusinessController BusinessController //TODO - add support of the Unity to the project
        {
            get
            {
                return BusinessController.Instance;
            }
        }

        [AllowAnonymous]
        public ActionResult LoginCard()
        {
            return View("LoginCardView");
        }

        [AllowAnonymous]
        public ActionResult CheckCardNumber(CardModel model) //TODO: add server side validation
        {
            var isValid = BusinessController.CheckCardNumber(model.CardNumber);
            var retVal = isValid ? View("CheckPinView", new PinModel { CardNumber = model.CardNumber }) : View("LoginCardView");
            return retVal;
        }

        [AllowAnonymous]
        public ActionResult CheckPin(PinModel model)
        {
            var isValid = BusinessController.CheckPinNumber(model.CardNumber, model.Pin);
            if (isValid)
            {
                var redirect = RedirectToAction("GetOperations", "Home");
                return redirect;
            }
            return View("CheckPinView", new PinModel { CardNumber = model.CardNumber, Pin = model.Pin });
        }

        [AllowAnonymous]
        public ActionResult GetOperations()
        {
            return View("OperationsView");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
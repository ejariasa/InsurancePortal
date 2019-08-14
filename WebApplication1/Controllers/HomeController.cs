using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business.ApiCalls;
using InsurancePortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.EntityModels;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        protected readonly ApiCalls ApiCall;
        public const string GetAllUrl = "api/Policy";
        public const string AddUrl = "api/Policy/AddPolicy";
        public const string UpdateUrl = "api/Policy/UpdatePolicy";
        public const string DeleteUrl = "api/Policy/DeletePolicy?id={0}";
        public const string GetAssigmentsUrl = "api/CustomerPolicy/GetCustomersPolicy?idPolicy={0}";
        public const string AssignUrl = "api/CustomerPolicy/AssignPolicy";

        public HomeController(ApiCalls apiCall)
        {
            this.ApiCall = apiCall;
        }
        
        public IActionResult Index()
        {
            var model = ApiCall.MakeGetAPICall<List<PolicyModel>>(GetAllUrl);
            return View(model);
        }

        public IActionResult AddPolicy()
        {
            var model = new PolicyViewModel();
            return View("EditPolicy", model);
        }

        public IActionResult LoadPolicy(int id)
        {
            var model = new PolicyViewModel();
            var list = ApiCall.MakeGetAPICall<List<PolicyModel>>(GetAllUrl);
            model.Policy = list.FirstOrDefault(x => x.Id == id);
            return View("EditPolicy", model);
        }
        public IActionResult EditPolicy(PolicyViewModel model)
        {
            if (model.Policy != null)
            {
                if (model.Policy.RiskType == RiskType.High.ToString())
                {
                    if (model.Policy.CoveragePecentage > 50)
                    {
                        ModelState.AddModelError("CoveragePecentage", "Coverage Percentage can not be higher than 50% for High Risk Type Policy.");
                        return View("EditPolicy", model);
                    }
                }
                model.Policy.StartDate = DateTime.Now;
                var obj = JsonConvert.SerializeObject(model.Policy);
                if (model.Policy.Id == 0)
                {

                    ApiCall.MakePostAPICall<int>(AddUrl, obj);
                }
                else
                {
                    ApiCall.MakePostAPICall<int>(UpdateUrl, obj);
                }

            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            ApiCall.MakePostAPICallNoJson<int>(string.Format(DeleteUrl, id));

            return RedirectToAction("Index");
        }

        public IActionResult AssignCustomer(int id, string name)
        {
            var model = new PolicyAssigmentViewModel();
            model.Name = name;
            model.Assigments = ApiCall.MakeGetAPICall<List<CustomerPolicyViewModel>>(string.Format(GetAssigmentsUrl, id));

            return View("AssignPolicy", model);
        }

        public ActionResult Assign(PolicyAssigmentViewModel model)
        {
            var obj = JsonConvert.SerializeObject(model.Assigments);
            ApiCall.MakePostAPICall<int>(AssignUrl, obj);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

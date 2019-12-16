using Frontend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class CarController : Controller
    {
        const string Baseurl = "https://localhost/";
        private readonly ITokenContainer tokenContainer;

        public CarController()
        {
            tokenContainer = new TokenContainer();
        }

        public CarController(ITokenContainer tokenContainer)
        {
            this.tokenContainer = tokenContainer;
        }
        // GET: Car
        public async Task<ActionResult> Index()
        {
            IEnumerable<CarModels> car;

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                if (tokenContainer.ApiToken == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenContainer.ApiToken.ToString());

                var request = new HttpRequestMessage(HttpMethod.Get, "api/Car");
                
                HttpResponseMessage Res = await client.SendAsync(request);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var resContent = Res.Content.ReadAsStringAsync().Result;

                    car = JsonConvert.DeserializeObject<IEnumerable<CarModels>>(resContent);

                    ViewBag.Username = tokenContainer.Username.ToString();
                    return View(car);
                }
                return RedirectToAction("Login", "Home");
            }
        }

        public async Task<ActionResult> GetImages(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                //if (claim == null)
                if (tokenContainer.ApiToken == null)
                {
                    return null;
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenContainer.ApiToken.ToString());

                var request = new HttpRequestMessage(HttpMethod.Get, "api/Car/" + id.ToString());

                HttpResponseMessage Res = await client.SendAsync(request);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var resContent = Res.Content.ReadAsByteArrayAsync().Result;

                    return File(resContent, "image/jpg");

                }
                return null;
            }
        }
    }
}
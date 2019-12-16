using Frontend.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        const string Baseurl = "https://localhost/";
        //private readonly ILoginClient loginClient;
        private readonly ITokenContainer tokenContainer;

        public HomeController()
        {
            tokenContainer = new TokenContainer();
        }

        public HomeController(ITokenContainer tokenContainer)
        {
            this.tokenContainer = tokenContainer;
        }

        public ActionResult Index()
        {
            ViewBag.Username = tokenContainer.Username == null ? "" : tokenContainer.Username.ToString();

            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Username = tokenContainer.Username == null ? "" : tokenContainer.Username.ToString();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Username = tokenContainer.Username == null ? "" : tokenContainer.Username.ToString();

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your login page.";

            LoginModels login = new LoginModels();

            return View(login);
        }
                
        [HttpPost]
        public async Task<ActionResult> Login(LoginModels login)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    var request = new HttpRequestMessage(HttpMethod.Post, "Token");

                    var keyValues = new List<KeyValuePair<string, string>>();
                    keyValues.Add(new KeyValuePair<string, string>("grant_type", "password"));
                    keyValues.Add(new KeyValuePair<string, string>("username", login.Username));
                    keyValues.Add(new KeyValuePair<string, string>("password", login.Password));

                    request.Content = new FormUrlEncodedContent(keyValues);

                    HttpResponseMessage Res = await client.SendAsync(request);

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var resContent = Res.Content.ReadAsStringAsync().Result;

                        Token token = JsonConvert.DeserializeObject<Token>(resContent);

                        AuthenticationProperties options = new AuthenticationProperties();

                        options.AllowRefresh = true;
                        options.IsPersistent = true;
                        options.ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.expires_in));

                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, login.Username),
                            new Claim("AccessToken", string.Format("Bearer {0}", token.access_token)),
                        };
                        tokenContainer.ApiToken = token.access_token;
                        tokenContainer.Username = token.userName;
                        var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                        Request.GetOwinContext().Authentication.SignIn(options, identity);

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        //EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                        ViewBag.Username = token.userName;
                        return RedirectToAction("Index", "Car");

                    }
                    return View(login);
                }
            }
            else
            {
                return View(login);
            }
            
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Your register page.";
            ViewBag.Username = tokenContainer.Username == null ? "" : tokenContainer.Username.ToString();

            LoginModels login = new LoginModels();

            return View(login);
        }

        [HttpPost]
        public async Task<ActionResult> Register(LoginModels login)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {


                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    //HTTP POST
                    //var postTask = client.PostAsJsonAsync<StudentViewModel>("student", student);
                    //postTask.Wait();

                    //var result = postTask.Result;
                    //if (result.IsSuccessStatusCode)
                    //{
                    //    return RedirectToAction("Index");
                    //}
                    //var body = "{\"grant_type\": \"password\",\"username\":" + login.Username +
                    //    ",\"password\":" + login.Password + "}";

                    var request = new HttpRequestMessage(HttpMethod.Post, "/api/Account/Register");

                    var keyValues = new List<KeyValuePair<string, string>>();
                    keyValues.Add(new KeyValuePair<string, string>("Email", login.Username));
                    keyValues.Add(new KeyValuePair<string, string>("Password", login.Password));
                    keyValues.Add(new KeyValuePair<string, string>("ConfirmPassword", login.ConfirmPassword));

                    request.Content = new FormUrlEncodedContent(keyValues);

                    //request.Content = new StringContent(new JavaScriptSerializer().Serialize(login), Encoding.UTF8, "application/json");

                    HttpResponseMessage Res = await client.SendAsync(request);
                    //HttpContent c = new StringContent(new JavaScriptSerializer().Serialize(login), Encoding.UTF8, "application/x-www-form-urlencoded");
                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    //HttpResponseMessage Res = await client.PostAsync("Token", c);

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        //var resContent = Res.Content.ReadAsStringAsync().Result;

                        //Token token = JsonConvert.DeserializeObject<Token>(resContent);

                        //AuthenticationProperties options = new AuthenticationProperties();

                        //options.AllowRefresh = true;
                        //options.IsPersistent = true;
                        //options.ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.expires_in));

                        //var claims = new[]
                        //    {
                        //    new Claim(ClaimTypes.Name, login.Username),
                        //    new Claim("AcessToken", string.Format("Bearer {0}", token.access_token)),
                        //};

                        //var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                        //Request.GetOwinContext().Authentication.SignIn(options, identity);

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        //EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);

                        return RedirectToAction("Login", "Home");

                    }
                    return View(login);
                }
            }
            else
            {
                return View(login);
            }            
        }

        [HttpPost]
        public async Task<ActionResult> LogOff()
        {
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

                var request = new HttpRequestMessage(HttpMethod.Post, "api/Account/LogOut");
                
                //request.Content = new FormUrlEncodedContent(keyValues);

                HttpResponseMessage Res = await client.SendAsync(request);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    tokenContainer.ApiToken = "";
                    tokenContainer.Username = "";
                }
                return RedirectToAction("Login", "Home");
            }
        }
    }
}
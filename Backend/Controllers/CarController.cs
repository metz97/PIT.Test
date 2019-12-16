using Backend.DataAccessLayer;
using Backend.Models.Car;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Backend.Controllers
{
    [Authorize]
    public class CarController : ApiController
    {
        private ICarRepository _repository;

        public CarController(ICarRepository repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<CarModels> Get()
        {
            return _repository.GetAll();
        }


        //public IHttpActionResult Get(int id)
        public HttpResponseMessage Get(int id)
        {
            var car = _repository.GetById(id);
            if (car == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            byte[] imgData = System.IO.File
                        .ReadAllBytes(
                          System.Web.Hosting.HostingEnvironment.MapPath(car.ImagePath));
            
            MemoryStream ms = new MemoryStream(imgData);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg");
            return response;
            //return Ok(response);
        }
    }
}
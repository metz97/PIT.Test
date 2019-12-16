using Backend.Models;
using Backend.Models.Car;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Backend.DataAccessLayer
{
    public interface ICarRepository
    {
        IEnumerable<CarModels> GetAll();
        CarModels GetById(int id);
        void Add(CarModels car);
    }

    public class CarRepository : ICarRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<CarModels> GetAll()
        {
            return db.Cars;
        }
        public CarModels GetById(int id)
        {
            return db.Cars.FirstOrDefault(p => p.Id == id);
        }
        public void Add(CarModels car)
        {
            db.Cars.Add(car);
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //public CarModels GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
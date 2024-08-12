using ShyamDhokiya_557_Model.DbContext;
using ShyamDhokiya_557_Model.ViewModel;
using ShyamDhokiya_557_Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Repository.Service
{
    public class BikeService : BikeRepository
    {
        private readonly ShyamDhokiya_557Entities db = new ShyamDhokiya_557Entities();
        public bool AddBike(BikeModel bikeModel)
        {
            try
            {
                int save = 0;
                Seller seller = new Seller()
                {
                    FirstName = bikeModel.FirstName,
                    LastName = bikeModel.LastName,
                    Email = bikeModel.Email,
                    PhoneNumber = bikeModel.PhoneNumber
                };

                seller = db.Seller.Add(seller);
                save = db.SaveChanges();

                Bike bike = new Bike()
                {
                    Brand = bikeModel.Brand,
                    Models = bikeModel.Models,
                    kilometresDriven = bikeModel.kilometresDriven,
                    Price = bikeModel.Price,
                    SellerId = seller.SellerId,
                    Years = bikeModel.Years,
                    Description = bikeModel.Description,
                    Image = bikeModel.Image,
                    IsDelete = false
                };

                db.Bike.Add(bike);
                save = db.SaveChanges();

                return save > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BikeModel> BikeList()
        {
            List<BikeModel> list = new List<BikeModel>();
            list = db.Database.SqlQuery<BikeModel>("exec GetBikeList").ToList();
            return list;
        }

        public bool DeleteBike(int BikeId)
        {
            int save = 0;
            Bike bike = db.Bike.Where(m => m.BikeId == BikeId).FirstOrDefault();
            bike.IsDelete = true;

            db.Entry(bike).State = System.Data.Entity.EntityState.Modified;
            save = db.SaveChanges();

            return save > 0 ? true : false;
        }

        public bool EditBike(BikeModel bikeModel)
        {
            int save = 0;
            Bike bike = db.Bike.Where(m => m.BikeId == bikeModel.BikeId).FirstOrDefault();
            Seller seller = db.Seller.Where(m => m.SellerId == bikeModel.SellerId).FirstOrDefault();

            bike.Brand = bikeModel.Brand;
            bike.Models = bikeModel.Models;
            bike.kilometresDriven = bikeModel.kilometresDriven;
            bike.Price = bikeModel.Price;
            bike.SellerId = seller.SellerId;
            bike.Years = bikeModel.Years;
            bike.Description = bikeModel.Description;
            bike.Image = bikeModel.Image;
            db.Entry(bike).State = System.Data.Entity.EntityState.Modified;
            save = db.SaveChanges();

            db.Entry(seller).State = System.Data.Entity.EntityState.Modified;
            seller.FirstName = bikeModel.FirstName;
            seller.LastName = bikeModel.LastName;
            seller.Email = bikeModel.Email;
            seller.PhoneNumber = bikeModel.PhoneNumber;
            save  = db.SaveChanges();

            return save > 0 ? true : false;
        }

        public BikeModel GetBikeById(int BikeId)
        {
            BikeModel bikeModel = new BikeModel();
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@BikeId",BikeId)
            };

            bikeModel = db.Database.SqlQuery<BikeModel>("exec GetBikeById @BikeId", sqlParameters).FirstOrDefault();
            return bikeModel;
        }

        public string GetImageByBikeId(int BikeId)
        {
            try
            {
                Bike bike = db.Bike.Where(m => m.BikeId == BikeId).FirstOrDefault();
                string Iamge = bike.Image;
                return Iamge;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

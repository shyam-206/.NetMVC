using ShyamDhokiya_557_API.JWTAuthencation;
using ShyamDhokiya_557_Model.ViewModel;
using ShyamDhokiya_557_Repository.Repository;
using ShyamDhokiya_557_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShyamDhokiya_557_API.Controllers
{
    [JwtAuthencation]
    public class MainAPIController : ApiController
    {
        private readonly BikeRepository repo = new BikeService();
        
        [Route("api/Main/AddBike")]
        public bool AddBike(BikeModel bikeModel)
        {
            return repo.AddBike(bikeModel);
        }

        [HttpGet]
        [Route("api/Main/BikeList")]
        public List<BikeModel> BikeList()
        {
            return repo.BikeList();
        }

        [HttpGet]
        [Route("api/Main/GetBikeById")]
        public BikeModel GetBikeById(int BikeId)
        {
            return repo.GetBikeById(BikeId);
        }

        [Route("api/Main/EditBike")]
        public bool EditBike(BikeModel bikeModel)
        {
            return repo.EditBike(bikeModel);
        }

        [Route("api/Main/DeleteBike")]
        [HttpGet]
        public bool DeleteBike(int BikeId)
        {
            return repo.DeleteBike(BikeId);
        }

        [Route("api/Main/GetImageByBikeId")]
        public string GetImageByBikeId(int BikeId)
        {
            return repo.GetImageByBikeId(BikeId);
        }

    }
}
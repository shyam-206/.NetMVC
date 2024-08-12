using ShyamDhokiya_557_Model.DbContext;
using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Repository.Repository
{
    public interface BikeRepository
    {
        bool AddBike(BikeModel bikeModel);

        List<BikeModel> BikeList();

        BikeModel GetBikeById(int BikeId);

        bool EditBike(BikeModel bikeModel);

        bool DeleteBike(int BikeId);

        string GetImageByBikeId(int BikeId);

    }
}

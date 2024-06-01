using CourseManagement_Model.DBContext;
using CourseManagement_Model.ViewModel;
using CourseManagement_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Repository.Service
{
    public class InstructorService : InstructorRepository
    {
        private readonly CourseManagement557Entities _context = new CourseManagement557Entities();

        public bool UploadMaterial(MaterialModel materialModel)
        {
            try
            {
                int CheckSaveMaterial = 0;
                Material material = new Material
                {
                    CourseId = materialModel.CourseId,
                    Title = materialModel.Title,
                    FilePath = materialModel.FilePath,
                    Created_at = DateTime.Now
                };

                _context.Material.Add(material);
                CheckSaveMaterial = _context.SaveChanges();
                return CheckSaveMaterial > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

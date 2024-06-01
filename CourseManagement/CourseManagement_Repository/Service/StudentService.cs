using CourseManagement_Helper.Helper;
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
    public class StudentService : IStudentRepository
    {
        private readonly CourseManagement557Entities _context = new CourseManagement557Entities();

        public bool EnrollmentCourse(int CourseId, int UserId)
        {
            try
            {
                int CheckEnrollCourse = 0;
                Enrollment enrollment = new Enrollment
                {
                    CourseId = CourseId,
                    UserId = UserId,
                    Created_at = DateTime.Now
                };

                _context.Enrollment.Add(enrollment);
                CheckEnrollCourse = _context.SaveChanges();
                return CheckEnrollCourse > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CourseModel> GetAllCourseList(int UserId)
        {
            try
            {
                List<Course> courses = _context.Course.ToList();
                List<CourseModel> list = CourseHelper.ConvertCourseModelListToCourseList(courses);
                foreach (var item in list)
                {
                    item.IsEnrollment = _context.Enrollment.FirstOrDefault(m => m.CourseId == item.CourseId && m.UserId == UserId) != null ? true : false;
                }
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MaterialModel GetMaterial(int CourseId)
        {
            try
            {
                Material material = _context.Material.FirstOrDefault(m => m.CourseId == CourseId);
                MaterialModel materialModel = new MaterialModel();
                if (material != null)
                {

                    materialModel.MaterialId = material.MaterialId;
                    materialModel.CourseId = (int)material.CourseId;
                    materialModel.Title = material.Title;
                    materialModel.FilePath = material.FilePath;

                }

                return materialModel != null ? materialModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

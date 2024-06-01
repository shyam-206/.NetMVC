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
    public class CourseService : ICourseRepository
    {
        private readonly CourseManagement557Entities _context = new CourseManagement557Entities();
        public bool AddCourse(CourseModel courseModel)
        {
            try
            {
                int CheckCourseSave = 0;
                Course course = CourseHelper.ConvertCourseModelToCourse(courseModel);

                _context.Course.Add(course);
                CheckCourseSave = _context.SaveChanges();
                return CheckCourseSave > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CourseModel> GetCourseList(int InstructorId = 2)
        {
            try
            {
                List<Course> CourseList = _context.Course.Where(m => m.InstructorId == InstructorId).ToList();
                List<CourseModel> courseModelList = CourseHelper.ConvertCourseModelListToCourseList(CourseList);
                return courseModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

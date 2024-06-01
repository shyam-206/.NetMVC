using CourseManagement_Model.DBContext;
using CourseManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Helper.Helper
{
    public class CourseHelper
    {
        public static Course ConvertCourseModelToCourse(CourseModel courseModel)
        {
            try
            {
                Course course = new Course
                {
                    Title = courseModel.Title,
                    Description = courseModel.Description,
                    InstructorId = courseModel.InstructorId,
                    Created_at = DateTime.Now
                };

                return course;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<CourseModel> ConvertCourseModelListToCourseList(List<Course> courses)
        {
            try
            {
                List<CourseModel> courseModelList = new List<CourseModel>();
                if(courses != null)
                {
                    foreach(var course in courses)
                    {
                        CourseModel courseModel = new CourseModel();
                        courseModel.CourseId = course.CourseId;
                        courseModel.Title = course.Title;
                        courseModel.Description = course.Description;
                        courseModel.InstructorId = (int)course.InstructorId;
                        courseModelList.Add(courseModel);
                    }
                }
                return courseModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

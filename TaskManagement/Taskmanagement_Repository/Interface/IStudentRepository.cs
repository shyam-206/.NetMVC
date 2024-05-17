using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;

namespace Taskmanagement_Repository.Interface
{
    public interface IStudentRepository
    {
        bool AddStudent(RegisterModel registerModel);

        List<AssignmentModelList> GetAllTaskAssignByTeacher(int StudentId);

        bool AssignmentStatusUpdate(int id);

        int GetTaskAssignByTeacherCount(int studentId);

        int GetCompleteTaskCount(int studentId);

        int GetPendingTaskCount(int studentId);

        List<AssignmentModelList> GetCompleteTaskList(int studentId);
        List<AssignmentModelList> GetPendingTaskList(int studentId);
    }
}

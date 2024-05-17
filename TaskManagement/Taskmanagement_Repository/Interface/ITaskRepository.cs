using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;

namespace Taskmanagement_Repository.Interface
{
    public interface ITaskRepository
    {
        bool AddTask(TaskModel taskModel);
        List<StudentModel> GetAllStudentList(int TaskId);

        List<TaskModel> GetAllTaskList(int teacherId);

        bool AssignTask(AssignmentModel assignmentModel);
        int GetTotaTaskCount(int teacherId);

        int GetAllAssignTaskCount(int teacherId);

        int CompletedTaskCount(int teacherId);

        int PendingTaskCount(int teacherId);

        TaskModel GetTaskByTaskId(int TaskID);

        List<AssignmentModelList> CompleteTaskList(int teacherId);
        List<AssignmentModelList> PendingTaskList(int teacherId);

        List<AssignmentModelList> AssignTaskList(int teacherId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;

namespace TaskManagement_Helper.Helper
{
    public class AssignmentHelper
    {
        public static List<AssignmentModelList> ConvertDBAssignmentListToAssignmentModelList(List<Assignment> assignments)
        {
            List<AssignmentModelList> assignmentModelLists = new List<AssignmentModelList>();
            if(assignments != null)
            {
                foreach (var item in assignments)
                {
                    AssignmentModelList assignmentModel = new AssignmentModelList();
                    assignmentModel.AssignmentID = item.AssignmentID;
                    assignmentModel.StudentID = item.Student.StudentID;
                    assignmentModel.TaskID = item.TaskID;
                    assignmentModel.Status = item.Status;
                    assignmentModel.StudentName = item.Student.Username;
                    assignmentModel.TaskName = item.Task.TaskName;
                    assignmentModel.Description = item.Task.Description;
                    assignmentModel.Deadline = (DateTime)item.Task.Deadline;
                    assignmentModel.TeacherName = item.Task.Teacher.Username;
                    assignmentModelLists.Add(assignmentModel);
                }
            }
            return assignmentModelLists;
        }
    }
}

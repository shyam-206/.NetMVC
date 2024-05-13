using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;

namespace TaskManagement_Helper.Helper
{
    public class TaskHelper
    {
        public static Task ConvertTaskModeltoTask(TaskModel taskModel)
        {
            try
            {
                Task task = new Task();

                task.TaskID = taskModel.TaskID;
                task.TaskName = taskModel.TaskName;
                task.Description = taskModel.Description;
                task.Deadline = taskModel.Deadline;
                task.CreatorID = taskModel.CreatorID;

                return task;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static TaskModel ConvertTasktoTaskModel(Task task)
        {
            try
            {
                TaskModel taskModel = new TaskModel();

                taskModel.TaskID = task.TaskID;
                taskModel.TaskName = task.TaskName;
                taskModel.Description = task.Description;
                taskModel.Deadline = task.Deadline;
                taskModel.CreatorID = task.CreatorID;

                return taskModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public static List<TaskModel> ConvertTaskListToTaskModelList(List<Task> tasks)
        {
            try
            {
                List<TaskModel> taskModelList = new List<TaskModel>();

                if (tasks != null)
                {
                    foreach (var task in tasks)
                    {
                        TaskModel taskModel = new TaskModel();
                        taskModel.TaskID = task.TaskID;
                        taskModel.TaskName = task.TaskName;
                        taskModel.Description = task.Description;
                        taskModel.Deadline = task.Deadline;
                        taskModel.CreatorID = task.CreatorID;
                        taskModelList.Add(taskModel);
                    }
                }

                return taskModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

﻿using System;
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
        List<StudentModel> GetAllStudentList();

        List<TaskModel> GetAllTaskList();

        bool AssignTask(AssignmentModel assignmentModel);
    }
}
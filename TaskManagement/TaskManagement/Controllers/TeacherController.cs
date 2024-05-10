﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagement.CustomFilter;
using TaskManagement.Session;
using TaskManagement_Model.ViewModel;
using Taskmanagement_Repository.Interface;
using Taskmanagement_Repository.Service;

namespace TaskManagement.Controllers
{
    [CustomAuthorization]
    [CustomErrorMessage]
    public class TeacherController : Controller
    {
        private readonly ITaskRepository taskRepository;

        public TeacherController()
        {
            taskRepository = new TaskService();
        }
        // GET: Teacher
        public ActionResult Index()
        {
            try
            {
                int teacherId = SessionHelper.UserId;
                ViewBag.TotalTaskCount = taskRepository.GetTotaTaskCount(teacherId);
                ViewBag.AssignTaskCount = taskRepository.GetAllAssignTaskCount(teacherId);
                ViewBag.CompletedTaskCount = taskRepository.CompletedTaskCount(teacherId);
                ViewBag.PendingTaskCount = taskRepository.PendingTaskCount(teacherId);
                return View();
            }
            catch (Exception ex)
            {

                    throw ex;
            }
        }

        [HttpGet]
        public ActionResult Addtask()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Addtask(TaskModel taskModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isCheckingSaveOrNot = taskRepository.AddTask(taskModel);
                    if (isCheckingSaveOrNot)
                    {
                        TempData["Addtask"] = "New Task Added";
                        return RedirectToAction("Index", "Teacher");
                    }
                }
                return View(taskModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            /*return View(taskModel);*/
        }

        public ActionResult Assigntask()
        {
            try
            {
                int teacherId = SessionHelper.UserId;
                List<TaskModel> taskModelList = new List<TaskModel>();
                
                ViewBag.AllStudentList = taskRepository.GetAllStudentList();
                ViewBag.TaskList = taskRepository.GetAllTaskList(teacherId);
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public ActionResult Tasklist()
        {
            try
            {
                List<TaskModel> taskModelList = taskRepository.GetAllTaskList(SessionHelper.UserId);
                return View(taskModelList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Assigntask(AssignmentModel assignmentModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isCheckSave = taskRepository.AssignTask(assignmentModel);
                    if (isCheckSave)
                    {
                        TempData["Assgintask"] = "Task is Assign to Student";
                        return RedirectToAction("Index");
                    }
                }
                int teacherId = SessionHelper.UserId;
                ViewBag.AllStudentList = taskRepository.GetAllStudentList();
                ViewBag.TaskList = taskRepository.GetAllTaskList(teacherId);
                return View(assignmentModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult PageNotFound()
        {
            return View("Error");
        }
    }
}
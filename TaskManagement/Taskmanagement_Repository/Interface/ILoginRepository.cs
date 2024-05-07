﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.ViewModel;

namespace Taskmanagement_Repository.Interface
{
    public interface ILoginRepository
    {
        bool CheckingStudentExist(RegisterModel _loginUserModel);

        bool CheckingTeacherExist(RegisterModel _loginUserModel);

    }
}
   using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Helper.Helper;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;
using Taskmanagement_Repository.Interface;

namespace Taskmanagement_Repository.Service
{
    public class StateService : IStateRepository
    {
        private readonly TaskManagement_557Entities _context;

        public StateService()
        {
            _context = new TaskManagement_557Entities();
        }

        public List<StateModel> stateModelList()
        {
            List<StateModel> stateModelList = new List<StateModel>();
            List<State> states = _context.State.ToList();

            stateModelList = StateHelper.ConvertStateToStateModel(states);
            return stateModelList;
        }
    }
}

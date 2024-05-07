using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;

namespace TaskManagement_Helper.Helper
{
    public class StateHelper
    {
        public static List<StateModel> ConvertStateToStateModel(List<State> stateList)
        {
            try
            {
                List<StateModel> stateModelList = new List<StateModel>();
                if(stateList != null)
                {
                    foreach(var state in stateList)
                    {
                        StateModel stateModel = new StateModel();
                        stateModel.StateID = state.StateID;
                        stateModel.StateName = state.StateName;
                        stateModelList.Add(stateModel);
                    }
                }
                return stateModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

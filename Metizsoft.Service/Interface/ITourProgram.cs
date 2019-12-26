using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service
{
  public interface ITourProgram
    {
      bool AddTourProgram(TourProgram_Mst obj);

      List<TourProgram_MstModel> GetTourprogramMonthlylist(string Month, string Year, long CurrentUserId);

      List<TourProgram_Varianceresponce> GetVarianceList(string CurrentUserId);

      bool ApproveTour(long TourId, long ApprovedBy, string ApprovedStatus);

      List<TourPlanResponse> GetTourPlanVarianceHistory();

      bool DeleteTourProgram(long TourID);

      List<UserListViewModel> GetUserByManagerId(int UserId, int UserRoleId);

      List<TourPlanResponse> GetAllTourList(TourPlanResponse model);

      bool ApproveMR(long TourID, string IsApproved, int ApprovedBy);

      bool RejectMR(long TourID, string IsApproved, int ApprovedBy);

      bool UpdateAllTourApprove(List<TourPlanResponse> data);

      bool UpdateAllTourRejected(List<TourPlanResponse> data);

      List<TourPlanModel> GetTourList(string UserId);
    }
}

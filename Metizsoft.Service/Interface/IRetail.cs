
namespace Metizsoft.Service
{
    using Metizsoft.Data.Modal;
    using System.Collections.Generic;
    using Metizsoft.Service.Interface;
    using Metizsoft.Data;
    using Metizsoft.Service.Models;
    using Metizsoft.Data.ViewModal;

    public interface IRetail
    {
         bool AddRetailer(Retailer_mst ObjRetailer);

         //List<RetailerMst_mst> GetAllRetailerList();
         List<RetailerMst_mst> GetAllRetailerList();

         List<RetailerViewModel> GetAllRetailerListforApi(int UserId, string City, string SubArea);

         bool DeleteRetailer(long ReatailerId, bool IsActive);

          List<RetailerDetailViewModel> GetallRetailerDetail(int UserType);

          bool AddRetailerReport(RetailerReport_Mst objretailrreportmst, List<DoctorProductReport_Mst> lstdpr);

          RetailerMst_mst GetRetailerByRetailerId(int ReatailerId);

          List<RetailerMst_mst> GetAllRetailerListUnapprovedByRSM(int UserId);

          int GetApprovedByRollId(int UserID);

          bool ApproveRetailerRSM(int ReatailerId, int UserId, int Status);

          List<Retailer_mst> GetAllRetailerList(long GroupID);

          List<Retailer_mst> GetAllRetailerList(long GroupID, long GroupTypeID);

          List<RCPAResponse> GetRCPAReport();

          List<RetailerDCRViewModel> GetRetailerDCRReport();

          long GetLastRetailerCode();

          List<RetailerMst_mst> GetAllRetailerListForFilter(RetailerMst_mst model);
    }
}

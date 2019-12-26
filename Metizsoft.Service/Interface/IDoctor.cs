namespace Metizsoft.Service
{
    using Metizsoft.Data.Modal;
    using System.Collections.Generic;
    using Metizsoft.Service.Interface;
    using Metizsoft.Data;
    using Metizsoft.Service.Models;
    using System;
    using Metizsoft.Data.ViewModal;

    public interface IDoctor
    {
        bool AddDoctor(Doctor_Mst ObjDoctor);

        List<Doctor_MstModel> GetAllDoctorList();

        bool DeleteDoctor(long DoctorId, bool IsActive);

        int GetApprovedByRollId(int UserID);

        List<DoctorSummaryReport> DoctorSummaryReport(int CurrentUserId, DateTime? StartDate, DateTime? EndDate);

        ProductViewModel1 GetallProductName();

        DoctorViewModel GetallDoctorName(int UserId, string Area, string SubArea);

        bool AddDoctorReport(DoctorReport_Mst objdoctorreportmst, List<DoctorProductReport_Mst> objdoctorreport);

        List<ProductListViewModel> GetallProductNameList(int ProductId);

        List<DoctorReportViewModel> GetDoctorReport();

        List<DoctorFieldUserResponse> GetDoctorFieldUserReport();

        List<DoctorListResponse> GetDoctorListReport();

        List<RCPA> RCPA(int CurrentUserId, DateTime? StartDate, DateTime? EndDate);
        List<PreCallAnalysisReport> PreCallAnalysisReport(int CurrentUserId, DateTime? StartDate, DateTime? EndDate);

        Doctor_MstModel GetDoctorByDoctorId(int DoctorId);

        List<Doctor_MstModel> GetAllDoctorListApprovedByRSM(int UserId);

        bool ApproveDoctorRSM(int DoctorId, int UserId, int Status);

        List<Doctor_MstModel> GetAllDoctorList(long GroupID);

        List<Doctor_MstModel> GetAllDoctorList(long GroupID, long GroupTypeID);

        //List<ProductListViewModel> GetallProductNameList(int ProductId);

        //List<DoctorReportViewModel> GetDoctorReport();

        List<PrecallResponse> GetPreCallAnalysisReport();

        List<DoctorDetailsResponce> GetDoctorDetailsReport();

        List<DoctorProductResponse> GetDoctorProductReport();

        List<DoctorRetailerResponse> GetDoctorRetailerReport();

        List<DoctorDCRViewModel> GetDoctorDCRReport();

        long GetLastDoctorCode();

        List<Doctor_MstModel> GetAllDoctorListForFilter(Doctor_MstModel model);

        List<MeetingTypeModel> GetAvailableMeeting(int UserId, int DoctorId);
    }
}

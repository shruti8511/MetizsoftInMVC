
namespace Metizsoft.Service
{
    using Metizsoft.Data.Modal;
    using System.Collections.Generic;
    using Metizsoft.Service.Interface;
    using Metizsoft.Data;
    using Metizsoft.Service.Models;
    using Metizsoft.Data.ViewModal;
    using System;

    public interface IMaster
    {
        bool AddWorkType(WorkType_mst objworktype);

        List<WorkType_mst> GetAllWorkTypelist();

        bool DeleteWorkType(long WorkTypeId, bool IsActive);

        bool AddArea(Area_Mst objarea);

        List<Area_Mst> GetAllArealist();

        bool DeleteArea(long AreaID, bool IsActive);

        AreaViewModel1 GetAllAreaSubAreaList();

        WorkAndWorkWithModel GetAllWorkAndWorkWith();

        List<WorkTypeViewModel> GetallWorkTypeName();

        List<WorkWitheViewModel> GetallWorkWithName();

        List<AreaViewModel> GetallAreaName();

        List<SubAreaViewModel> GetallSubAreaList();

        List<TourProgram_MstModel> GetDCRFilterlist(DateTime date, long createdby);

        long AddDCR(DCR_Mst objdcr);

        List<DCRViewModel> GetDCRReport();

        bool AddPromotional(Promotional_Mst objpromst);
        List<Promotional_Mst> GetAllPromotionallist();
        bool DeletePromotional(long PromotionalId, bool IsActive);
        List<PromotionalViewModel> GetallPromotionalName();
        bool AddPromotional(PromotionalReport_Mst objpromaster);
        List<PromotionalReportViewModel> GetPromotionalReport();

        List<DCRSummaryReport> DCRSummaryReport(int CurrentUserId, DateTime? StartDate, DateTime? EndDate);

        bool AddEvent(EventType_Mst objeventtype);
        List<EventType_Mst> GetAllEventTypelist();
        bool DeleteEvent(long EventTypeID, bool IsActive);

        bool AddRxType(RxType_mst objrxtype);
        List<RxType_mst> GetAllRxTypelist();
        bool DeleteRxType(long RxTypeId, bool IsActive);

        List<MeetingType_mst> GetAllMeetingTypelist();
        bool AddMeetingType(MeetingType_mst objworktype);
        bool DeleteMeetingType(long MeetingTypeId, bool IsActive);

        bool AddProductType(ProductType_Mst objproducttype);
        List<ProductType_Mst> GetAllProductTypelist();
        bool DeleteProductType(long ProductTypeId, bool IsActive);

        bool AddLeaveType(LeaveType_Mst objleavetype);
        List<LeaveType_Mst> GetAllLeaveTypelist();
        bool DeleteLeaveType(long LeaveTypeId, bool IsActive);

       
    }
}

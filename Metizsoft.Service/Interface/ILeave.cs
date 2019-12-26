namespace Metizsoft.Service
{
    using Metizsoft.Data.Modal;
    using System.Collections.Generic;
    using Metizsoft.Service.Interface;
    using Metizsoft.Data;
    using Metizsoft.Service.Models;
    using Metizsoft.Data.ViewModal;
    using Metizsoft.Model;
    using System;


    public interface ILeave
    {
        long AddLeave(LeaveMaster obj);
        ResponseLeaveCountModel GetallLeavesByUserId(string userId, long roleId, DateTime StartDate, DateTime EndDate);
      //  List<User> GetMRByASM(int UserType);
        bool ApproveLeaveASM(long LeaveId, string Status);
        bool ApproveLeaveVP(long LeaveId, string Status);
        List<Register> GetUnapprovedForASM(); 
        List<Register> GetUnapprovedForVP();
        List<LeaveResponse> GetLeaveReport();

        List<LeaveTypeResponse> GetAllLeaveTypeName();
    }
}

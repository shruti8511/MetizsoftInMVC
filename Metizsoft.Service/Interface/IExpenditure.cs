namespace Metizsoft.Service
{
    using Metizsoft.Data.Modal;
    using System.Collections.Generic;
    using Metizsoft.Service.Interface;
    using Metizsoft.Data;
    using Metizsoft.Service.Models;
    using Metizsoft.Data.ViewModal;
    using System;

    public interface IExpenditure
    {
        bool AddExpenditure(Expenser_Mst objexpen);

        List<Expenser_Mst> GetAllExpenditureList();

        bool DeleteExpenditure(long ExpenserId);

        bool AddExpenditureReport(ExpenserReport_Mst objExpenditurereportmst);

        List<ExpenditureViewModel> GetallExpenditureList(int CreatedBy, DateTime? StartDate, DateTime? EndDate);

        List<ExpenditureResponse> GetExpenditureReport();
    }
}

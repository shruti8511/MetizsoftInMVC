namespace Metizsoft.Service
{
    using Metizsoft.Data.Modal;
    using Metizsoft.Data.ViewModal;
    using Metizsoft.Model;
    using Metizsoft.Service.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public interface IAccount
    {
        string ResetPassword(long UserID);
        bool ActiveDeactive(activedeactive Data);
    }
}

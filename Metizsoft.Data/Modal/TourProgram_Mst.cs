//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Metizsoft.Data.Modal
{
    using System;
    using System.Collections.Generic;
    
    public partial class TourProgram_Mst
    {
        public long TourID { get; set; }
        public string Day { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<long> AreaID { get; set; }
        public Nullable<long> SubareaID { get; set; }
        public string HQRS { get; set; }
        public string ContactPoint { get; set; }
        public string IsApproved { get; set; }
        public Nullable<long> ApprovedBy { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}

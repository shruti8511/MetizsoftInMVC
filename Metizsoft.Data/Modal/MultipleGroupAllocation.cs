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
    
    public partial class MultipleGroupAllocation
    {
        public long ID { get; set; }
        public Nullable<long> AllocationID { get; set; }
        public Nullable<long> GroupTypeID { get; set; }
        public Nullable<long> GroupID { get; set; }
        public Nullable<long> ToAllocateID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}

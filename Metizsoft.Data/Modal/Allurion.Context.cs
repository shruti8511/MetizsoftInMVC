﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AllurionEntities : DbContext
    {
        public AllurionEntities()
            : base("name=AllurionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Area_Mst> Area_Mst { get; set; }
        public virtual DbSet<AssignedLeave> AssignedLeaves { get; set; }
        public virtual DbSet<AuthorizeMaster> AuthorizeMasters { get; set; }
        public virtual DbSet<DCR_Mst> DCR_Mst { get; set; }
        public virtual DbSet<Doctor_Mst> Doctor_Mst { get; set; }
        public virtual DbSet<DoctorProductReport_Mst> DoctorProductReport_Mst { get; set; }
        public virtual DbSet<DoctorReport_Mst> DoctorReport_Mst { get; set; }
        public virtual DbSet<DynamicMenuMaster> DynamicMenuMasters { get; set; }
        public virtual DbSet<Event_Mst> Event_Mst { get; set; }
        public virtual DbSet<EventType_Mst> EventType_Mst { get; set; }
        public virtual DbSet<Expenser_Mst> Expenser_Mst { get; set; }
        public virtual DbSet<ExpenserReport_Mst> ExpenserReport_Mst { get; set; }
        public virtual DbSet<Group_Mst> Group_Mst { get; set; }
        public virtual DbSet<GroupAllocation_Mst> GroupAllocation_Mst { get; set; }
        public virtual DbSet<GroupToFieldUser_Mst> GroupToFieldUser_Mst { get; set; }
        public virtual DbSet<GroupType_Mst> GroupType_Mst { get; set; }
        public virtual DbSet<LeaveMaster> LeaveMasters { get; set; }
        public virtual DbSet<LeaveType_Mst> LeaveType_Mst { get; set; }
        public virtual DbSet<ManageBalloon> ManageBalloons { get; set; }
        public virtual DbSet<MeetingType_mst> MeetingType_mst { get; set; }
        public virtual DbSet<MultipleGroupAllocation> MultipleGroupAllocations { get; set; }
        public virtual DbSet<NotificationManageMaster> NotificationManageMasters { get; set; }
        public virtual DbSet<NotificationMaster> NotificationMasters { get; set; }
        public virtual DbSet<Product_Mst> Product_Mst { get; set; }
        public virtual DbSet<Productallocation> Productallocations { get; set; }
        public virtual DbSet<ProductCase_Mst> ProductCase_Mst { get; set; }
        public virtual DbSet<ProductType_Mst> ProductType_Mst { get; set; }
        public virtual DbSet<Promotional_Mst> Promotional_Mst { get; set; }
        public virtual DbSet<PromotionalReport_Mst> PromotionalReport_Mst { get; set; }
        public virtual DbSet<Retailer_mst> Retailer_mst { get; set; }
        public virtual DbSet<RetailerReport_Mst> RetailerReport_Mst { get; set; }
        public virtual DbSet<RxType_mst> RxType_mst { get; set; }
        public virtual DbSet<SubUserMaster> SubUserMasters { get; set; }
        public virtual DbSet<TourAreaProgram_Mst> TourAreaProgram_Mst { get; set; }
        public virtual DbSet<TourProgram_Mst> TourProgram_Mst { get; set; }
        public virtual DbSet<Tracker_Mst> Tracker_Mst { get; set; }
        public virtual DbSet<User_Info> User_Info { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
        public virtual DbSet<webpages_Membership> webpages_Membership { get; set; }
        public virtual DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public virtual DbSet<WorkType_mst> WorkType_mst { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<webpages_Roles> webpages_Roles { get; set; }
    }
}

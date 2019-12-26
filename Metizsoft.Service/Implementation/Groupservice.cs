using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service.Implementation
{
    public class Groupservice : IGroup
    {
        public bool AddGroup(Group_Mst objproduct)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objproduct.GroupID == 0)
                {
                    context.Group_Mst.Add(objproduct);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objproduct).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objproduct.GroupID > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<GroupModel> GetAllGroupList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllGrouplist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<GroupModel> lstProduct = new List<GroupModel>();
            while (dr.Read())
            {
                GroupModel objProductemst = new GroupModel();
                objProductemst.GroupID = objBaseSqlManager.GetInt32(dr, "GroupID");
                objProductemst.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objProductemst.GroupTypeName = objBaseSqlManager.GetTextValue(dr, "GroupTypeName");
                objProductemst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                objProductemst.GroupTypeID = objBaseSqlManager.GetInt64(dr, "GroupTypeID");
                objProductemst.GroupCode = objBaseSqlManager.GetTextValue(dr, "GroupCode");
                lstProduct.Add(objProductemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        public bool DeleteGroup(long GroupID)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                Group_Mst data = context.Group_Mst.FirstOrDefault(x => x.GroupID == GroupID);
                if (data != null)
                {
                    data.IsActive = false;
                    context.Group_Mst.Remove(data);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public List<Group_Mst> SelectAllGroup()
        {
            List<Group_Mst> lstGroup = new List<Group_Mst>();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            SqlCommand cmdGet = new SqlCommand();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "SelectAllGroup";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {
                Group_Mst objEntity = new Group_Mst();
                objEntity.GroupID = objBaseSqlManager.GetInt64(dr, "GroupID");
                objEntity.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objEntity.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");

                lstGroup.Add(objEntity);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstGroup;
        }

        public List<Group_Mst> SelectGroup(long GroupID)
        {
            using (AllurionEntities Context = new AllurionEntities())
            {

                var data = Context.Group_Mst.Where(i => i.IsActive == true && i.GroupTypeID == GroupID).ToList();
                return data;
            }
        }

        public List<GroupType_Mst> SelectGroupType()
        {
            using (AllurionEntities Context = new AllurionEntities())
            {

                var data = Context.GroupType_Mst.Where(i => i.IsActive == true).ToList();
                return data;
            }
        }

        public List<Group_Mst> SelectGroupTypeForProduct()
        {
            using (AllurionEntities Context = new AllurionEntities())
            {

                var data = Context.Group_Mst.Where(i => i.IsActive == true && i.GroupTypeID == 3).ToList();
                return data;
            }
        }

        //public List<GroupType_Mst> SelectGroupTypeForFieldUser()
        //{
        //    using (AllurionEntities Context = new AllurionEntities())
        //    {

        //        var data = Context.GroupType_Mst.Where(i => i.IsActive == true && i.GrouptypeID == 1).ToList();
        //        return data;
        //    }
        //}

        public List<GroupViewModel> GetallGroupNameList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallGroupNameList";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<GroupViewModel> lstDoctor = new List<GroupViewModel>();
            while (dr.Read())
            {
                GroupViewModel objDocotrmst = new GroupViewModel();
                objDocotrmst.GrouptypeID = objBaseSqlManager.GetInt32(dr, "GrouptypeID");
                objDocotrmst.GroupTypeName = objBaseSqlManager.GetTextValue(dr, "GroupTypeName");

                lstDoctor.Add(objDocotrmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstDoctor;
        }

        public bool GroupAllocation(ALlocationModel data)
        {
            try
            {
                using (AllurionEntities context = new AllurionEntities())
                {
                    #region ALocation
                    var existDoctor = context.GroupAllocation_Mst.Where(i => i.GroupID == data.GroupID && i.GroupTypeID == data.GroupTypeID && i.AllocationID == data.ID).FirstOrDefault();
                    if (existDoctor == null && data.IsActive == true)
                    {
                        GroupAllocation_Mst objinsert = new GroupAllocation_Mst();
                        objinsert.GroupID = data.GroupID;
                        objinsert.GroupTypeID = data.GroupTypeID;
                        objinsert.AllocationID = data.ID;
                        objinsert.CreatedOn = data.CreatedOn;
                        objinsert.CreatedBy = data.CreatedBy;
                        objinsert.UpdatedOn = data.UpdateOn;
                        objinsert.UpdatedBy = data.UpdateBy;
                        objinsert.IsActive = data.IsActive;
                        context.GroupAllocation_Mst.Add(objinsert);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        existDoctor.GroupID = data.GroupID;
                        existDoctor.GroupTypeID = data.GroupTypeID;
                        existDoctor.AllocationID = data.ID;
                        existDoctor.CreatedOn = existDoctor.CreatedOn;
                        existDoctor.CreatedBy = existDoctor.CreatedBy;
                        existDoctor.UpdatedOn = data.UpdateOn;
                        existDoctor.UpdatedBy = data.UpdateBy;
                        existDoctor.IsActive = data.IsActive;
                        context.Entry(existDoctor).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                    #endregion
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Group_MstModel> GetAlloctedList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllAssignList";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Group_MstModel> lstDoctor = new List<Group_MstModel>();
            while (dr.Read())
            {
                Group_MstModel objDocotrmst = new Group_MstModel();
                objDocotrmst.Name = objBaseSqlManager.GetTextValue(dr, "name");
                objDocotrmst.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objDocotrmst.GroupID = objBaseSqlManager.GetInt64(dr, "GroupID");
                objDocotrmst.GroupTypeID = objBaseSqlManager.GetInt64(dr, "GroupTypeID");
                objDocotrmst.AllocationID = objBaseSqlManager.GetInt64(dr, "AllocationID");
                objDocotrmst.UpdateOnstring = Convert.ToDateTime(objBaseSqlManager.GetDateTime(dr, "UpdatedOn")).ToString("dd-MM-yyyy");
                objDocotrmst.CreatedOnstring = Convert.ToDateTime(objBaseSqlManager.GetDateTime(dr, "CreatedOn")).ToString("dd-MM-yyyy");
                objDocotrmst.CreateUserName = objBaseSqlManager.GetTextValue(dr, "CreateUserName");
                objDocotrmst.GroupTypeName = objBaseSqlManager.GetTextValue(dr, "GroupTypeName");
                objDocotrmst.UpdateUserName = objBaseSqlManager.GetTextValue(dr, "UpdateUserName");

                lstDoctor.Add(objDocotrmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstDoctor;
        }


        public bool DeleteAlloction(long GroupID, long GroupTypeID, long AllocationID)
        {
            try
            {
                using (AllurionEntities context = new AllurionEntities())
                {
                    #region ALocation
                    var existDoctor = context.GroupAllocation_Mst.Where(i => i.GroupID == GroupID && i.GroupTypeID == GroupTypeID && i.AllocationID == AllocationID).FirstOrDefault();
                    if (existDoctor != null)
                    {
                        existDoctor.IsActive = false;
                        context.Entry(existDoctor).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    return true;

                    #endregion
                }
                //  return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<MultipleGroup_MstModel> MultipleGetAlloctedList(long GroupTypeID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "MultipleGetAllAssignList";
            cmdGet.Parameters.AddWithValue("@GroupTypeID", GroupTypeID);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<MultipleGroup_MstModel> lstDoctor = new List<MultipleGroup_MstModel>();
            while (dr.Read())
            {
                MultipleGroup_MstModel objDocotrmst = new MultipleGroup_MstModel();
                objDocotrmst.Id = objBaseSqlManager.GetInt32(dr, "ID");
                objDocotrmst.Name = objBaseSqlManager.GetTextValue(dr, "name");
                objDocotrmst.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objDocotrmst.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
                objDocotrmst.GroupID = objBaseSqlManager.GetInt64(dr, "GroupID");
                objDocotrmst.GroupTypeID = objBaseSqlManager.GetInt64(dr, "GroupTypeID");
                if (objDocotrmst.GroupTypeID == 1)
                {
                    objDocotrmst.Name = "Doctor - Product";
                }
                else if (objDocotrmst.GroupTypeID == 2)
                {
                    objDocotrmst.Name = "Retailer - Product";
                }
                objDocotrmst.ToAllocateID = objBaseSqlManager.GetInt64(dr, "ToAllocateID");
                objDocotrmst.UpdateOnstring = Convert.ToDateTime(objBaseSqlManager.GetDateTime(dr, "UpdatedOn")).ToString("dd-MM-yyyy");
                objDocotrmst.CreatedOnstring = Convert.ToDateTime(objBaseSqlManager.GetDateTime(dr, "CreatedOn")).ToString("dd-MM-yyyy");
                objDocotrmst.CreateUserName = objBaseSqlManager.GetTextValue(dr, "CreateUserName");
                objDocotrmst.GroupTypeName = objBaseSqlManager.GetTextValue(dr, "GroupTypeName");
                objDocotrmst.UpdateUserName = objBaseSqlManager.GetTextValue(dr, "UpdateUserName");

                lstDoctor.Add(objDocotrmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstDoctor;
        }

        public bool GroupMultipleAllocation(MultipleAllocation data)
        {
            try
            {
                using (AllurionEntities context = new AllurionEntities())
                {
                    #region ALocation

                    var existDoctor = context.MultipleGroupAllocations.Where(i =>i.ID == data.ID).FirstOrDefault();
                   // var existDoctor = context.MultipleGroupAllocations.Where(i => i.GroupID == data.GroupID && i.ID == data.ID && i.GroupTypeID == data.GroupTypeID && i.AllocationID == data.UserID).FirstOrDefault();
                    if (existDoctor == null && data.IsActive == true)
                    {
                        MultipleGroupAllocation objinsert = new MultipleGroupAllocation();
                        objinsert.GroupID = data.GroupID;
                        objinsert.GroupTypeID = data.GroupTypeID;
                        objinsert.ToAllocateID = data.UserID;
                        objinsert.CreatedOn = data.CreatedOn;
                        objinsert.CreatedBy = data.CreatedBy;
                        objinsert.UpdatedOn = data.UpdateOn;
                        objinsert.UpdatedBy = data.UpdateBy;
                        objinsert.IsActive = data.IsActive;
                        context.MultipleGroupAllocations.Add(objinsert);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        existDoctor.GroupID = data.GroupID;
                        existDoctor.GroupTypeID = data.GroupTypeID;
                        existDoctor.ToAllocateID = data.UserID;
                        existDoctor.CreatedOn = existDoctor.CreatedOn;
                        existDoctor.CreatedBy = existDoctor.CreatedBy;
                        existDoctor.UpdatedOn = data.UpdateOn;
                        existDoctor.UpdatedBy = data.UpdateBy;
                        existDoctor.IsActive = data.IsActive;
                        context.Entry(existDoctor).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                    #endregion

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteAlloctionGroup(long GroupID, long GroupTypeID, long AllocationID)
        {
            try
            {
                using (AllurionEntities context = new AllurionEntities())
                {
                    #region ALocation
                    var existDoctor = context.MultipleGroupAllocations.Where(i => i.GroupID == GroupID && i.GroupTypeID == GroupTypeID && i.ToAllocateID == AllocationID).FirstOrDefault();
                    if (existDoctor != null)
                    {
                        existDoctor.IsActive = false;
                        context.Entry(existDoctor).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    return true;

                    #endregion


                }
                //  return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<GroupreportResponse> GetGroupReport()
        {
            List<GroupreportResponse> objects = new List<GroupreportResponse>();
            return objects;
        }
        public List<GroupDetailsResponse> GetGroupDetailsReport()
        {
            List<GroupDetailsResponse> objects = new List<GroupDetailsResponse>();
            return objects;
        }

        public List<GetUserByPosition> GetUserNameByPosition(int RoleId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUserNameByPosition";
            cmdGet.Parameters.AddWithValue("@RoleId", RoleId);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<GetUserByPosition> objlstuser = new List<GetUserByPosition>();
            while (dr.Read())
            {
                GetUserByPosition objUser = new GetUserByPosition();
                objUser.Id = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objlstuser.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstuser;
        }

        public bool AddGroupToFieldUser(GroupToFieldUser_Mst objgroup)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objgroup.Id == 0)
                {
                    context.GroupToFieldUser_Mst.Add(objgroup);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objgroup).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objgroup.Id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<GroupToFieldUserModel> GetGroupToFieldUserlist()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetGroupToFieldUserlist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<GroupToFieldUserModel> lstgroup = new List<GroupToFieldUserModel>();
            while (dr.Read())
            {
                GroupToFieldUserModel objgroup = new GroupToFieldUserModel();
                objgroup.Id = objBaseSqlManager.GetInt32(dr, "Id");
                objgroup.UserId = objBaseSqlManager.GetInt32(dr, "UserId");
                objgroup.UserName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objgroup.UserPositionId = objBaseSqlManager.GetInt32(dr, "UserRoleID");
                objgroup.UserPosition = objBaseSqlManager.GetTextValue(dr, "RoleName");
                objgroup.GroupID = objBaseSqlManager.GetInt32(dr, "GroupID");
                objgroup.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objgroup.GrouptypeID = objBaseSqlManager.GetInt32(dr, "GrouptypeID");
                objgroup.GroupTypeName = objBaseSqlManager.GetTextValue(dr, "GroupTypeName");
                objgroup.CreateUserName = objBaseSqlManager.GetTextValue(dr, "Createdname");
                objgroup.UpdateUserName = objBaseSqlManager.GetTextValue(dr, "Updatedname");
                objgroup.UpdateOnstr = Convert.ToDateTime(objBaseSqlManager.GetDateTime(dr, "UpdatedOn")).ToString("dd-MM-yyyy");
                objgroup.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstgroup.Add(objgroup);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstgroup;
        }

        public bool DeleteGroupToFieldUser(long Id, bool IsActive)
        {
            try
            {
                using (AllurionEntities context = new AllurionEntities())
                {

                    var exist = context.GroupToFieldUser_Mst.Where(i => i.Id == Id && i.IsActive == IsActive).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.IsActive = false;
                        context.Entry(exist).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public long GetLastGroupCode()
        {
            long GrpId = 0;
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetLastGroupCode";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {
                GrpId = objBaseSqlManager.GetInt64(dr, "GroupID");
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return GrpId;
        }
    }
}

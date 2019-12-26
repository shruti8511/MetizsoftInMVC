using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service
{
    public interface IGroup 
    {
        bool AddGroup(Group_Mst objGroup);

         bool DeleteAlloction(long GroupTypeID ,long GroupID,long AllocationID);

        //List<Group_Mst> Group_MstModel();

        bool DeleteGroup(long GroupId);

        List<Group_Mst> SelectAllGroup();

        List<Group_MstModel> GetAlloctedList();

         List<GroupType_Mst> SelectGroupType();

         List<Group_Mst> SelectGroup(long GroupID);

         List<GroupModel> GetAllGroupList();

         bool GroupAllocation(ALlocationModel Data);

         List<MultipleGroup_MstModel> MultipleGetAlloctedList(long GroupTypeID);

         bool GroupMultipleAllocation(MultipleAllocation Data);

         bool DeleteAlloctionGroup(long GroupID, long GroupTypeID, long AllocationID);

         List<GroupreportResponse> GetGroupReport();

         List<GroupDetailsResponse> GetGroupDetailsReport();

         List<GetUserByPosition> GetUserNameByPosition(int RoleId);

         bool AddGroupToFieldUser(GroupToFieldUser_Mst objgroup);

         List<GroupToFieldUserModel> GetGroupToFieldUserlist();

         bool DeleteGroupToFieldUser(long Id, bool IsActive);

         List<Group_Mst> SelectGroupTypeForProduct();

         long GetLastGroupCode();
    }
}

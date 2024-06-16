using System.Collections.Generic;
using MoneyMinder.Models;

namespace MoneyMinder.Interfaces
{
    public interface IAdminUserRelationRepository
    {
        ICollection<AdminUserRelation> GetAdminUserRelations();
        AdminUserRelation GetAdminUserRelation(int adminId, int userId);
        bool AdminUserRelationExists(int adminId, int userId);
        bool CreateAdminUserRelation(AdminUserRelation relation);
        bool DeleteAdminUserRelation(AdminUserRelation relation);
        bool Save();
    }
}
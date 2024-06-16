using System.Collections.Generic;
using MoneyMinder.Models;

namespace MoneyMinder.Interfaces
{
    public interface IAdminRepository
    {
        ICollection<Admin> GetAdmins();
        Admin GetAdmin(int id);
        Admin GetAdmin(string username);
        bool AdminExists(int id);
        bool CreateAdmin(Admin admin);
        bool UpdateAdmin(Admin admin);
        bool DeleteAdmin(Admin admin);
        bool Save();
    }
}
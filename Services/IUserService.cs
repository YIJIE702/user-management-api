using System;
using System.Collections.Generic;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User? GetById(Guid id);
        User Create(User user);
        bool Update(Guid id, User user);
        bool Delete(Guid id);
        bool EmailExists(string email, Guid? excludeId = null);
    }
}

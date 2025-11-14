using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    // Simple thread-safe in-memory user store for demo purposes
    public class UserService : IUserService
    {
        private readonly ConcurrentDictionary<Guid, User> _store = new();

        public UserService()
        {
            // Seed with a sample user
            var sample = new User
            {
                FirstName = "Alice",
                LastName = "Example",
                Email = "alice@example.com",
                Role = "admin"
            };
            _store.TryAdd(sample.Id, sample);
        }

        public IEnumerable<User> GetAll() => _store.Values.OrderBy(u => u.CreatedAt);

        public User? GetById(Guid id) => _store.TryGetValue(id, out var u) ? u : null;

        public User Create(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            _store[user.Id] = user;
            return user;
        }

        public bool Update(Guid id, User user)
        {
            if (!_store.ContainsKey(id)) return false;
            user.Id = id;
            user.CreatedAt = _store[id].CreatedAt;
            _store[id] = user;
            return true;
        }

        public bool Delete(Guid id) => _store.TryRemove(id, out _);

        public bool EmailExists(string email, Guid? excludeId = null)
        {
            var q = _store.Values.Any(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase) && u.Id != excludeId);
            return q;
        }
    }
}

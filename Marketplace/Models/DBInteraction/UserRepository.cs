using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Models.DBInteraction
{
    public class UserRepository
    {
        private MarketplaceContext ctx;
        private DbSet<User> users;

        public UserRepository(MarketplaceContext context)
        {
            ctx = context;
            users = context.Users;
        }

        public User GetById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public User GetByEmail(string email)
        {
            return users.FirstOrDefault(u => u.Email == email);
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User GetAdmin()
        {
            return users.FirstOrDefault(u => u.RoleId == 1);
        }

        public void Create(User u)
        {
            users.Add(u);
            ctx.SaveChanges();
        }

        public void Ban(int id)
        {
            User target = GetById(id);
            target.IsBanned = true;
            ctx.SaveChanges();
        }

        public void Unban(int id)
        {
            User target = GetById(id);
            target.IsBanned = false;
            ctx.SaveChanges();
        }
    }
}

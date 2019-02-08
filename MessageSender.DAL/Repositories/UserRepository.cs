using MessageSender.DAL.Context;
using MessageSender.DAL.Entities;
using MessageSender.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MessageSender.DAL.Repositories
{
	public class UserRepository : UserManager<User>, IRepository<User>
	{
		private ApplicationContext context;
		private DbSet<User> dbSet;

		public UserRepository(IUserStore<User> store, ApplicationContext context) : base(store)
		{
			this.context = context;
		}

		public void Add(User entity)
		{
			dbSet.Add(entity);
		}

		public void Delete(object id)
		{
			User entityToDelete = dbSet.Find(id);
			Delete(entityToDelete);
		}

		public void Delete(User entity)
		{
			if (context.Entry(entity).State == EntityState.Detached)
			{
				dbSet.Attach(entity);
			}
			dbSet.Remove(entity);
		}

		public IEnumerable<User> GetAll(Func<User, bool> filter = null)
		{
			if (filter != null)
			{
				return dbSet.Where(filter);
			}

			return dbSet.AsEnumerable();
		}

		public User GetByID(object id)
		{
			return dbSet.Find(id);
		}

		public void Update(User entityToUpdate)
		{
			dbSet.Attach(entityToUpdate);
			context.Entry(entityToUpdate).State = EntityState.Modified;
		}

		public User FindByPhone(string phone)
		{
			User user = context.Users.FirstOrDefault(p => p.PhoneNumber == phone);
			return user;
		}

	}
}

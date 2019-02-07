﻿using MessageSender.DAL.Context;
using MessageSender.DAL.Entities;
using MessageSender.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using MessageSender.DAL.Identity;

namespace MessageSender.DAL.Repositories
{
	public class IdentityUnitOfWork : IUnitOfWork
	{
		private ApplicationContext db;

		private ApplicationUserManager userManager;
		//private ApplicationRoleManager roleManager;
		private IClientManager clientManager;

		public IdentityUnitOfWork(string connectionString)
		{
			db = new ApplicationContext(connectionString);
			userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db), connectionString);
			//roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
			clientManager = new ClientManager(db);
		}

		public ApplicationUserManager UserManager
		{
			get { return userManager; }
		}

		public IClientManager ClientManager
		{
			get { return clientManager; }
		}

		//public ApplicationRoleManager RoleManager
		//{
		//	get { return roleManager; }
		//}

		public async Task SaveAsync()
		{
			await db.SaveChangesAsync();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					userManager.Dispose();
					//roleManager.Dispose();
					clientManager.Dispose();
				}
				disposed = true;
			}
		}
	}
}
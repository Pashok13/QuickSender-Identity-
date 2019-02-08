using MessageSender.DAL.Context;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace MessageSender.DAL.Repositories
{
	public class IdentityUnitOfWork : IDisposable
	{
		//private UserManager userManager;
		//private PhoneManager phoneManager;
		//private MessageManager messageManager;

		//public IdentityUnitOfWork(string connectionString)
		//{
		//	userManager		= new UserManager(connectionString);
		//	phoneManager	= new PhoneManager(connectionString);
		//	messageManager	= new MessageManager(connectionString);
		//}

		//public UserManager()
		//{
		//	get { return userManager}
		//}

		//public void Save()
		//{
		//	userManager.SaveChanges();
		//	phoneManager.SaveChanges();
		//	messageManager.SaveChanges();
		//}

		//public void Dispose()
		//{ 
		//	throw new NotImplementedException();
		//}

		private ApplicationContext db;

		private UserManager userManager;
		//private ApplicationRoleManager roleManager;

		public IdentityUnitOfWork(string connectionString)
		{
			db = new ApplicationContext(connectionString);
			userManager = new UserManager(connectionString);
		}

		public UserManager UserManager
		{
			get { return userManager; }
		}

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
					//userManager.Dispose();
				}
				disposed = true;
			}
		}

		//public virtual void Dispose(bool disposing)
		//{
		//	if (!disposed)
		//	{
		//		if (disposing)
		//		{
		//			userManager.Dispose();
		//			phoneManager.Dispose();
		//		}
		//		disposed = true;
		//	}
		//}
	}
}
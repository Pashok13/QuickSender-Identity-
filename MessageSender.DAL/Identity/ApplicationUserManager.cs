using MessageSender.DAL.Context;
using MessageSender.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace MessageSender.DAL.Identity
{
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		public ApplicationContext Database { get; set; }

		public ApplicationUserManager(IUserStore<ApplicationUser> store)
				: base(store)
		{
		}

		public ApplicationUserManager(IUserStore<ApplicationUser> store, string connectionString)
		: base(store)
		{
			Database = new ApplicationContext(connectionString);
		}

		public ApplicationUser FindByPhone(string phone)
		{
			ApplicationUser user = Database.Users.FirstOrDefault(p => p.PhoneNumber == phone);
			return user;
		}

	}
}

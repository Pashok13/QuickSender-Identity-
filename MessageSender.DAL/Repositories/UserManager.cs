using MessageSender.DAL.Context;
using MessageSender.DAL.Entities;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace MessageSender.DAL.Repositories
{
	public class UserManager 
	{
		public ApplicationContext Database { get; set; }

		public UserManager(string connectionString)
		{
			Database = new ApplicationContext(connectionString);
		}

		public User FindByPhone(string phone)
		{
			User user = Database.Users.FirstOrDefault(p => p.PhoneNumber == phone);
			return user;
		}

		public void SaveChanges()
		{
			Database.SaveChanges();
		}

	}
}

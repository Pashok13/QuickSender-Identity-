using MessageSender.DAL.Context;
using MessengeSending.Models;
using System.Linq;

namespace MessageSender.DAL.Repositories
{
	public class PhoneManager
	{
		public ApplicationContext Database { get; set; }

		public PhoneManager(string connectionString)
		{
			Database = new ApplicationContext(connectionString);
		}

		public void Create(string number)
		{
			Phone record = new Phone() { Number = number };
			Database.Phones.Add(record);
			Database.SaveChanges();
		}

		public Phone FindByPhone(string number)
		{
			Phone record = Database.Phones.FirstOrDefault(p => p.Number == number);
			return record;
		}

		public void SaveChanges()
		{
			Database.SaveChanges();
		}
	}
}

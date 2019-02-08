using MessageSender.DAL.Context;
using MessengeSending.Models;
using System.Linq;

namespace MessageSender.DAL.Repositories
{
	public class PhoneRepository : GenericRepository<Phone>
	{
		public PhoneRepository(ApplicationContext context) : base(context)
		{
		}

		public void CreateByPhone(string number)
		{
			Phone record = new Phone() { Number = number };
			context.Phones.Add(record);
			context.SaveChanges();
		}

		public Phone FindByPhone(string number)
		{
			Phone record = context.Phones.FirstOrDefault(p => p.Number == number);
			return record;
		}
	}
}

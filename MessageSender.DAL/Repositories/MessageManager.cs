using MessageSender.DAL.Context;

namespace MessageSender.DAL.Repositories
{
	class MessageManager
	{
		public ApplicationContext Database { get; set; }

		public MessageManager(string connectionString)
		{
			Database = new ApplicationContext(connectionString);
		}

		public void SaveChanges()
		{
			Database.SaveChanges();
		}
	}
}

using MessageSender.DAL.Context;
using MessengeSending.Models;

namespace MessageSender.DAL.Repositories
{
	public class MessageRepository : GenericRepository<Message>
	{
		public MessageRepository(ApplicationContext context) : base(context)
		{
		}
	}
}

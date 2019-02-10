using MessageSender.BLL.DTO;
using MessageSender.BLL.Infrastructure;
using MessengeSending.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender.BLL.Interfaces
{
	public interface IMessageService : IDisposable
	{
		Task<OperationDetails> Create(MessageDTO message, string userId);
		Task<Message> Send(MessageDTO message);
		Task<Message> Delete(string id);
	}
}

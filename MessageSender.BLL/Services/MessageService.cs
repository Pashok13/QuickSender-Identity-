using MessageSender.BLL.DTO;
using MessageSender.BLL.Infrastructure;
using MessageSender.BLL.Interfaces;
using MessageSender.DAL.Repositories;
using MessengeSending.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender.BLL.Services
{
	class MessageService : IMessageService
	{
		UnitOfWork Database { get; set; }

		public MessageService(UnitOfWork uow)
		{
			Database = uow;
		}

		public async Task<OperationDetails> Create(MessageDTO messageDTO, string userID)
		{
			Message message = new Message() { UserId = userID, TextMessage = messageDTO.TextMessage };

			Database.MessageRepository.Add(message);

			return new OperationDetails(true, "Registration successfully", userID);
		}

		public Task<Message> Delete(string id)
		{
			throw new NotImplementedException();
		}

		public Task<Message> Send(MessageDTO message)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			Database.Dispose();
		}
	}
}

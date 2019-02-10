using MessengeSending.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender.BLL.DTO
{
	public class MessageDTO
	{
		public int MessageId		{ get; set; }
		public string UserId		{ get; set; }
		public string TextMessage	{ get; set; }
		public DateTime CreateDate	{ get; set; }
		public DateTime StartDate	{ get; set; }
		public DateTime EndDate		{ get; set; }
		public DateTime Period		{ get; set; }
		List<Phone> Recepients		{ get; set; }

		public MessageDTO()
		{
			Recepients = new List<Phone>();
		}
	}
}

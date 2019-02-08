using MessengeSending.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessageSender.DAL.Entities
{
	public class User : IdentityUser
	{
		public ICollection<Message> Messages { get; set; }

		public User()
		{
			Messages = new List<Message>();
		}
	}
}

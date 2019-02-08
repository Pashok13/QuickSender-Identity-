using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MessageSender.DAL.Entities;
using MessengeSending.Models;

namespace MessageSender.DAL.Context
{
	public class ApplicationContext : IdentityDbContext<User>
	{
		public ApplicationContext(string conectionString) : base(conectionString) 
		{ }

		public DbSet<Message> Messages		{ get; set; }
		public DbSet<Phone> Phones			{ get; set; }
		public DbSet<AdditInfo> AdditInfo	{ get; set; }
		public DbSet<MessegeRecipient> MessegesRecipients { get; set; }
	}
}

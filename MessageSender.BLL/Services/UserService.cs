using MessageSender.BLL.DTO;
using MessageSender.BLL.Infrastructure;
using MessageSender.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using MessageSender.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MessageSender.DAL.Repositories;

namespace MessageSender.BLL.Services
{
	public class UserService : IUserService
	{
		UnitOfWork Database { get; set; }

		public UserService(UnitOfWork uow)
		{
			Database = uow;
		}

		public async Task<OperationDetails> Create(UserDTO userDto)
		{
			User user;

			if (Database.UserRepository.FindByEmail(userDto.Email) != null)
			{
				return new OperationDetails(false, "User with this e-mail are already exist", "Email");
			}
			else if (Database.UserRepository.FindByPhone(userDto.Phone) != null)
			{
				return new OperationDetails(false, "User with this phone number are already exist", "PhoneNumber");
			}
			else if(Database.UserRepository.FindByName(userDto.Login) != null)
			{
				return new OperationDetails(false, $"Name {userDto.Login} are already taken", "Login");
			}
			else
			{ 
				user = new User { Email = userDto.Email, UserName = userDto.Login, PhoneNumber = userDto.Phone };
				var result = await Database.UserRepository.CreateAsync(user, userDto.Password);

				if (result.Errors.Count() > 0)
					return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

				if (Database.PhoneRepository.FindByPhone(userDto.Phone) == null)
					Database.PhoneRepository.CreateByPhone(userDto.Phone);

				Database.Save();
				return new OperationDetails(true, "Registration successfully", user.Id);
			}		
		}

		public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
		{
			ClaimsIdentity claim = null;
			User user = await Database.UserRepository.FindAsync(userDto.Login, userDto.Password); 
			if (user != null)
				claim = await Database.UserRepository.CreateIdentityAsync(user,
											DefaultAuthenticationTypes.ApplicationCookie);
			return claim;
		}

		public async Task<ClaimsIdentity> ConfirmEmail(string id)
		{
			ClaimsIdentity claim = null;
			User user = Database.UserRepository.FindById(id);
			if (user != null)
			{
				user.EmailConfirmed = true;
				Database.Save();
				claim = await Database.UserRepository.CreateIdentityAsync(user,
											DefaultAuthenticationTypes.ApplicationCookie);
			}
			return claim;
		}

		public void Dispose()
		{
			Database.Dispose();
		}

		//// начальная инициализация бд
		//public async Task SetInitialData(UserDTO adminDto, List<string> roles)
		//{
		//	foreach (string roleName in roles)
		//	{
		//		var role = await Database.RoleManager.FindByNameAsync(roleName);
		//		if (role == null)
		//		{
		//			role = new ApplicationRole { Name = roleName };
		//			await Database.RoleManager.CreateAsync(role);
		//		}
		//	}
		//	await Create(adminDto);
		//}

	}
}
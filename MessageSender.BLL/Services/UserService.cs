﻿using MessageSender.BLL.DTO;
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
		IdentityUnitOfWork Database { get; set; }

		public UserService(IdentityUnitOfWork uow)
		{
			Database = uow;
		}

		public async Task<OperationDetails> Create(UserDTO userDto)
		{
			User user = await Database.UserManager.FindByEmailAsync(userDto.Email);

			if (user == null)
			{
				user = Database.UserManager.FindByPhone(userDto.Phone);
				if (user == null)
				{
					user = new User { Email = userDto.Email, UserName = userDto.Name, PhoneNumber = userDto.Phone };
					var result = await Database.UserManager.CreateAsync(user, userDto.Password);

					if (result.Errors.Count() > 0)
						return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

					if (Database.PhoneManager.FindByPhone(userDto.Phone) == null)
						Database.PhoneManager.Create(userDto.Phone);

					await Database.SaveAsync();
					return new OperationDetails(true, "Регистрация успешно пройдена", user.Id);
				}
				else 
				{
					return new OperationDetails(false, "User with this phone number are already exist", "PhoneNumber");
				}
			}
			else
			{
				return new OperationDetails(false, "User with this e-mail are already exist", "Email");
			}
		}

		public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
		{
			ClaimsIdentity claim = null;
			User user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password); 
			if (user != null)
				claim = await Database.UserManager.CreateIdentityAsync(user,
											DefaultAuthenticationTypes.ApplicationCookie);
			return claim;
		}

		public async Task<ClaimsIdentity> ConfirmEmail(string id)
		{
			ClaimsIdentity claim = null;
			User user = Database.UserManager.FindById(id);
			if (user != null)
			{
				user.EmailConfirmed = true;
				await Database.SaveAsync();
				claim = await Database.UserManager.CreateIdentityAsync(user,
											DefaultAuthenticationTypes.ApplicationCookie);
			}
			return claim;
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

		public void Dispose()
		{
			Database.Dispose();
		}
	}
}
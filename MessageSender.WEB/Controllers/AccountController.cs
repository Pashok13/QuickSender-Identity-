using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using MessageSender.BLL.DTO;
using System.Security.Claims;
using MessageSender.BLL.Interfaces;
using MessageSender.BLL.Infrastructure;
using MessageSender.Models;
using System.Net.Mail;

namespace MessageSender.Controllers
{
	public class AccountController : Controller
	{
		private IUserService UserService
		{
			get
			{
				return HttpContext.GetOwinContext().GetUserManager<IUserService>();
			}
		}

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
				ClaimsIdentity claim = await UserService.Authenticate(userDto);
				if (claim == null)
				{
					ModelState.AddModelError("", "Ivcorrect login or password");
				}
				else
				{
					AuthenticationManager.SignOut();
					AuthenticationManager.SignIn(new AuthenticationProperties
					{
						IsPersistent = true
					}, claim);
					return RedirectToAction("Index", "Home");
				}
			}
			return View(model);
		}

		public ActionResult Logout()
		{
			AuthenticationManager.SignOut();
			return RedirectToAction("Index", "Home");
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				UserDTO userDto = new UserDTO
				{
					Email = model.Email,
					Password = model.Password,
					Name = model.Name,
					Phone = model.PhoneNumber
					//Role = "user"
				};
				OperationDetails operationDetails = await UserService.Create(userDto);

				if (operationDetails.Succedeed)
				{
					// наш email с заголовком письма
					MailAddress from = new MailAddress("q.u.i.c.k.sender.r.r@gmail.com", "Quick sender");
					// кому отправляем
					MailAddress to = new MailAddress(model.Email);
					// создаем объект сообщения
					MailMessage m = new MailMessage(from, to);
					// тема письма
					m.Subject = "Email confirmation";
					// текст письма - включаем в него ссылку
					m.Body = string.Format("For complete registration, go to the link:" +
									"<a href=\"{0}\" title=\"Confirm email\">{0}</a>",
						Url.Action("ConfirmEmail", "Account", new { Token = operationDetails.Property, Email = model.Email }, Request.Url.Scheme));
					m.IsBodyHtml = true;
					// адрес smtp-сервера, с которого мы и будем отправлять письмо
					using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
					{
						// логин и пароль
						smtp.Credentials = new System.Net.NetworkCredential("q.u.i.c.k.sender.r.r@gmail.com", "quicksender123");
						smtp.EnableSsl = true;
						smtp.Send(m);
					}
					return RedirectToAction("Index", "Home");
					//return RedirectToAction("Confirm", "Account", new { Email = model.Email });
				}
				else
					ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
			}
			return View(model);
		}

		public async Task<ActionResult> ConfirmEmail(string Token, string Email)
		{
			ClaimsIdentity claim = await UserService.ConfirmEmail(Token);
			if (claim == null)
			{
				ModelState.AddModelError("", "I dont know what is wrong");
				return View();
			}
			else
			{
				AuthenticationManager.SignOut();
				AuthenticationManager.SignIn(new AuthenticationProperties
				{
					IsPersistent = true
				}, claim);
				return View();
			}
		}

		//private async Task SetInitialDataAsync()
		//{
		//	await UserService.SetInitialData(new UserDTO
		//	{
		//		Email = "admin@mail.ru",
		//		Password = "admin",
		//		Name = "Pasha",
		//		Role = "admin",
		//	}, new List<string> { "user", "admin" });
		//}

	}
}
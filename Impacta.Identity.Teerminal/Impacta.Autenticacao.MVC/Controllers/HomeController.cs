using Impacta.Autenticacao.MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Impacta.Autenticacao.MVC.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Inicio()
		{
			return View();
		}

		public ActionResult AreaLivre()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}


		[Authorize]
		public ActionResult AreaRestrita()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult LoginView()
		{
			ViewBag.Message = "Você esta na pagina de Login, seja Bem Vindo";

			return View();
		}

		public ActionResult CriarLogin()
		{
			Usuario usuario = null;

			return View(usuario);
		}

		[HttpPost]
		public ActionResult CriarLogin(Usuario usuario)
		{
			// bool resultado = SalvarUsuario(usuario);
			// if (resultado)

			if (SalvarUsuario (usuario))
			{
				return View("Inicio");
			}

			else
			{
				return View("CriarLogin");
			}


		}


		private bool SalvarUsuario(Usuario usuario)
		{
			bool retorno = false;


			var usuarioStore = new UserStore<IdentityUser>();
			var usuarioGerenciador =
			new UserManager<IdentityUser>(usuarioStore);

			var usuarioInfo = new IdentityUser() { UserName = usuario.UserName };

			IdentityResult resultado = usuarioGerenciador.Create(usuarioInfo, usuario.Password);



			if (resultado.Succeeded)
			{
				var gerenciadorDeAutenticacao = HttpContext.GetOwinContext().Authentication;

				var identidadeUsuario = usuarioGerenciador.CreateIdentity(usuarioInfo,

						DefaultAuthenticationTypes.ApplicationCookie);

				gerenciadorDeAutenticacao.SignIn(
					new AuthenticationProperties() { }, identidadeUsuario);

				retorno = true;
			}
			else
			{
				retorno = false;

				ViewBag.Erro = resultado.Errors;
			}

			return retorno;
		}

		[HttpPost]
		public ActionResult LoginView(Usuario usuario)
		{
			if (AutenticarUsuario (usuario))
			{
				return View("AreaRestrita");
			}

			else
			{
				return View("Inicio");
			}

		}

		private bool AutenticarUsuario(Usuario usuario)
		{
			bool retorno = false;

			var usuarioStore = new UserStore<IdentityUser>();
			var usuarioGerenciador =
			new UserManager<IdentityUser>(usuarioStore);


			var identidadeUsuario = usuarioGerenciador.Find(usuario.UserName, usuario.Password);

			if (identidadeUsuario != null)
			{
				var gerenciadorDeAutenticacao = HttpContext.GetOwinContext().Authentication;

				var identidade = usuarioGerenciador.CreateIdentity(identidadeUsuario,
				DefaultAuthenticationTypes.ApplicationCookie);
				gerenciadorDeAutenticacao.SignIn( new AuthenticationProperties()
				{ IsPersistent = false }, identidade);

				retorno = true;
			}
			else
			{
				ViewBag.MessagemErro= "Usuario ou senha invalida.";

				retorno = false;
			}

			return retorno;
		}


	}

}
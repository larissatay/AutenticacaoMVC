using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Impacta.Autenticacao.MVC.Models
{
	public class Usuario : IUser
	{
		public string Id { get; set; }
		[Required]
		public string UserName { get; set; }

		[Required]
		[MaxLength(8, ErrorMessage ="Maximo Permitido de 8 caracters")]
		public string Password { get; set; }
	}
}
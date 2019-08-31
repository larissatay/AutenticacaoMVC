using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impacta.Identity.Teerminal
{
	class Program
	{
		static void Main(string[] args)
		{
			// criar um usuario e senha 
			// que sera armazenado no banco de dados 
			// gerenciado pelo IDENTITY
			var nomeUsuario = "laridetony123@gmail.com";
			var senha = "Larissa1234";


			// vamos criar uma estrutura para receber o usuario 
			// e gerenciar as informações de autenticação 
			// para utilizar o identity e criar um usuario, precisamos
			// receber uma instancia de UserStore que é tipado com a Classe
			// IdentityUser (esta classe é esperado pelo EntityFramework)
			var usuarioArmazenado = new UserStore<IdentityUser>();
			// criar um objeto para fazer a gestao do usuario

			var usuarioGerenciador = new UserManager<IdentityUser>(usuarioArmazenado);

			IdentityUser objIdentityUser = new IdentityUser(nomeUsuario);
			var resultado = usuarioGerenciador.Create(objIdentityUser, senha);

			// as duas linhas são similares - fazem a mesma coisa, porem esta instancia diretamente
			// no metodo
			// var novo_resultado = usuarioGerenciador.Creat(new IdentityUser(nomeUsuario), senha);

			// verificar o status de retorno da criação do usuario
			Console.WriteLine("Status Creat {0}", resultado.Succeeded);
			Console.ReadLine();

			var identidadeUsuario = usuarioGerenciador.FindByName(nomeUsuario);

			// add Claim
			//usuarioGerenciador.AddClaim(identidadeUsuario.Id, new Claim("Nome_Usuario", "Larissa"));

			// Esta forma usar uma constante do Identit, para colocar a descrição
			// da Claim. Estas duas linhas fazem a mesma coisa.
			usuarioGerenciador.AddClaim(identidadeUsuario.Id, new Claim(ClaimTypes.GivenName, "Larissa"));

			// Vamos verificar se o password existe ou esta correto
			var validaSenha = usuarioGerenciador.CheckPassword(identidadeUsuario, senha);

			// Vamos escrever resultado da comparação da senha 
			Console.WriteLine("Senha verificada: {0}", validaSenha);

			Console.ReadLine();

		}
	}
}

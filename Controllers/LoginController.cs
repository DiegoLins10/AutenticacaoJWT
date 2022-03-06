using ApiAuth.Models;
using ApiAuth.Repositories;
using ApiAuth.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAuth.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
       [HttpPost]
       [Route("login")]
       public async Task <ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {
            // recupera o usuario
            var user = UserRepository.Get(model.Username, model.Password);

            //Verifica se o usuário existe
            if(user == null)
            {
                return NotFound(new { message = "Usuário ou senha invalidos" });
            }

            // Gera token 
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return Ok(new
            {
                user = user,
                token = token
            });
        }
    }
}

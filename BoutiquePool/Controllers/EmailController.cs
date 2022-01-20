using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Unieco.Services.Email;
using Unieco.Models;
using BoutiquePool.Models.Configurations.MongoDB;

namespace BoutiquePool.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;
        protected Repositories.MongoDB.PersistentRepository<Entities.Person> personRepository;

        public EmailController(IEmailSender emailSender, IWebHostEnvironment env, DatabaseSettings databaseSettings)
        {
            personRepository = new Repositories.MongoDB.PersistentRepository<Entities.Person>(databaseSettings, "person");
            _emailSender = emailSender;
        }
   
        [HttpPost("{email}")]
        public ActionResult EnviaEmail(string email)
        {
         

            var person = personRepository.FirstOrDefault(a => a.Email == email);
            if (person == null)
                return this.NotFound(new
                {
                    error = "o e-mail inserido não existe",
                    title = "ACCOUNT_NOT_FOUNDED",
                    message = "o email inserido, não existe em nossa base de dados, por isso não conseguimos mandar o e-mail de recuperação de senha",
                    status = 404,
                    instance = "/email/{email}"
                });


            var assunto = "Solicitação de recuperação de senha";
            var mensagem = "Olá " + person.Name + ", você solicitou a recuperação de sua senha do app unieco global, segue suas credenciais: <br /><br />" + "<b>nome de usuário: </b>"+ person.Username + "<br />" + "<b>senha: </b>" + person.Password;


            if (ModelState.IsValid)
            {
                try
                {
                    TesteEnvioEmail(email, assunto, mensagem).GetAwaiter();
                    return Ok(); 
                }
                catch (Exception)
                {
                    return Unauthorized("EmailFalhou");
                }
            }
            return View(email);
        }
        public async Task TesteEnvioEmail(string email, string assunto, string mensagem)
        {
            try
            {
                //email destino, assunto do email, mensagem a enviar
                await _emailSender.SendEmailAsync(email, assunto, mensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
    }
}
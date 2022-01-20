using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unieco.Services.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

    }
}

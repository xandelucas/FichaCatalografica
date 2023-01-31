﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MfcUfmt.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}

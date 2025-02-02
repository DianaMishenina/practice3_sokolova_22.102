using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace practice3_sokolova_22._102.Services
{
    /// <summary>
    /// Класс для отправки кода на почту сотрудника
    /// </summary>
    internal class SendEmail
    {
        /// <summary>
        /// Асинхронная функция для отравки письма с кодом для восстановления пароля на почту сотрудника
        /// </summary>
        /// <param name="email">Электронная почта сотрудника для отправки письма</param>
        /// <param name="code">Сгенерированный код, котрый будет указан в теле письма</param>
        public static async Task SendEmailAsync(string email, string code)
        {
            MailAddress from = new MailAddress("sokolovad46@yandex.ru", "Program");
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Восстановление пароля";
            message.Body = String.Format($"Код для восстановления пароля: {code}");

            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential("sokolovad46@yandex.ru", "lpzzzohtqzherazb");
            smtp.EnableSsl = true;

            await smtp.SendMailAsync(message);
        }

        /// <summary>
        /// Метод для отравки письма с кодом для двухфакторной аутентификации на почту сотрудника
        /// </summary>
        /// <param name="email">Электронная почта сотрудника для отправки письма</param>
        /// <param name="code">Сгенерированный код, котрый будет указан в теле письма</param>
        public static void SendEmailCode(string email, string code)
        {
            MailAddress from = new MailAddress("sokolovad46@yandex.ru", "Program");
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Двухфакторная аутентификация";
            message.Body = String.Format($"Код для двухфакторной аутентификации: {code}");

            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential("sokolovad46@yandex.ru", "lpzzzohtqzherazb");
            smtp.EnableSsl = true;

            smtp.Send(message);
        }
    }
}

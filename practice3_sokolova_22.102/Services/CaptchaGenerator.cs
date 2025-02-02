using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice3_sokolova_22._102.Services
{
    internal class CaptchaGenerator
    {
        private static readonly Random random = new Random();
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// Функция генерирующая капчу
        /// </summary>
        /// <param name="length">Длина капчи</param>
        /// <returns>Возвращает капчу определенной длины</returns>
        public static string GenerateCaptchaText(int length)
        {
            if (length <= 0)
                throw new ArgumentException("Длина текста капчи должна быть больше нуля.");

            StringBuilder captchaText = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(Characters.Length);
                captchaText.Append(Characters[index]);
            }

            return captchaText.ToString();
        }
    }
}

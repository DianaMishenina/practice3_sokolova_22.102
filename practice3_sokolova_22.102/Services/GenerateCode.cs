using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice3_sokolova_22._102.Services
{
    /// <summary>
    /// Класс для генерации кода восстановления пароля (двухфакторной аутентификации)
    /// </summary>
    public class GenerateCode
    {
        /// <summary>
        /// Функция, генерирующая четырехзачный код
        /// </summary>
        /// <returns>Возвращает случайное число от 1000 до 9999 включительно в строковом представлении</returns>
        public string CodeGenerate()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
}

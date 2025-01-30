using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice3_sokolova_22._102.Services
{
    public class GenerateCode
    {
        public string CodeGenerate()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
}

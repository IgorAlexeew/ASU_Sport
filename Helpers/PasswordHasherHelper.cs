using System.Security.Cryptography;
using System.Text;

namespace ASUSport.Helpers
{
    public class PasswordHasherHelper
    {
        public static string HashString(string data)
        {
            return BytesToString(SHA256.Create().ComputeHash(StringToBytes(data)));
        }

        /// <summary>
        /// Преобразование строки в байты
        /// </summary>
        /// <param name="str">Строка</param>
        /// <returns>Байтовое представление строки</returns>
        private static byte[] StringToBytes(string str)
            => Encoding.UTF8.GetBytes(str);

        /// <summary>
        /// Преобразование байт в строку
        /// </summary>
        /// <param name="bytes">Байты</param>
        /// <returns>Строка из байт</returns>
        private static string BytesToString(byte[] bytes)
        {
            var stringBuilder = new StringBuilder();
            foreach (byte theByte in bytes)
            {
                stringBuilder.Append(theByte.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

    }
}

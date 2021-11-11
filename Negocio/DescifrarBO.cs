using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WindowsLiderEntrega.Negocio
{
    public class DescifrarBO
    {
        /// <summary>
        /// Método para realizar el descifrado de información
        /// </summary>
        /// <param name="ClaveCifrado">Clave para realizar el descifrado</param>
        /// <param name="Cadena">texto que se quiere descifrar</param>
        /// <returns></returns>
        public string Desencripta(string ClaveCifrado, string Cadena)
        {
            //Este metodo no se requiere estructurar / optimizar
            byte[] Clave = Encoding.ASCII.GetBytes(ClaveCifrado);
            byte[] IV = Encoding.ASCII.GetBytes("1234567812345678");


            byte[] inputBytes = Convert.FromBase64String(Cadena);
            byte[] resultBytes = new byte[inputBytes.Length];
            string textoLimpio = String.Empty;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(Clave, IV), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(objCryptoStream, true))
                    {
                        textoLimpio = sr.ReadToEnd();
                    }
                }
            }
            return textoLimpio;
        }
    }
}

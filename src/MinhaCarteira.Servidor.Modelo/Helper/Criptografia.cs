using System;
using System.Linq;
using System.Security.Cryptography;

namespace MinhaCarteira.Servidor.Modelo.Helper
{
    public static class Criptografia
    {
        public static string GerarHashSenha(this string senha)
        {
            byte[] salt;
            byte[] buffer2;
            if (senha == null)
                throw new ArgumentNullException(nameof(senha));

            using (Rfc2898DeriveBytes bytes = new(senha, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerificarHashSenha(this string senhaCifrada, string senha)
        {
            byte[] buffer4;
            if (senhaCifrada == null)
                return false;

            if (senha == null)
                throw new ArgumentNullException(nameof(senha));

            byte[] src = Convert.FromBase64String(senhaCifrada);
            if ((src.Length != 0x31) || (src[0] != 0))
                return false;

            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new(senha, dst, 0x3e8))
                buffer4 = bytes.GetBytes(0x20);

            return Enumerable.SequenceEqual(buffer3, buffer4);
        }
    }
}

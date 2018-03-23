using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Infrastucture.Encrypter
{
    public interface IEncrypter
    {
        string GetSalt(string password);
        string GetHash(string password, string salt);
    }
}

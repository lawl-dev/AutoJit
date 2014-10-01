using System.Security.Cryptography;

namespace Lawl.Security
{
    public interface ISymmetricAlgorithmProvider
    {
        ICryptoTransform CreateEncryptor();
        ICryptoTransform CreateDecryptor();
    }
}
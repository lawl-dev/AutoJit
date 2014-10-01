using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lawl.Security
{
    public class SymmetricAlgorithmProvider<T> : ISymmetricAlgorithmProvider where T : SymmetricAlgorithm, new()
    {
        private readonly T _provider = new T();
        private byte[] _key;

        public byte[] Key
        {
            get { return _key ?? _provider.Key; }
            set
            {
                _key = value;
                _provider.Key = value;
            }
        }

        private byte[] _iv;

        public byte[] IV
        {
            get
            {
                return _iv ?? _provider.IV;
            }
            set
            {
                _iv = value;
                _provider.IV = value;
            }
        }

        public SymmetricAlgorithmProvider()
        {
            _provider.GenerateKey();
            _key = _provider.Key;
            _provider.GenerateIV();
            _iv = _provider.IV;
        }

        public SymmetricAlgorithmProvider(byte[] key, byte[] iv)
        {
            _key = key;
            _iv = iv;
        }

        public ICryptoTransform CreateEncryptor()
        {
            return _provider.CreateEncryptor();
        }

        public ICryptoTransform CreateDecryptor()
        {
            return _provider.CreateDecryptor();
        }

        public virtual void GenerateIV()
        {
            _provider.GenerateIV();
        }

        public virtual void GenerateKey()
        {
            _provider.GenerateKey();
        }
    }
}

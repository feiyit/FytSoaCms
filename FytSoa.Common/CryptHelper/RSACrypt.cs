using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using XC.Framework.Security.RSAUtil;

namespace FytSoa.Common
{
    /// <summary>
    /// RSA加密解密
    /// https://github.com/stulzq/RSAUtil
    /// </summary>
    public class RSACrypt {

        private readonly RsaPkcs1Util _RsaUtil;
        private readonly Encoding _encoding;

        /// <summary>
        /// 获得私钥和公钥
        /// [0]=privateKey  私钥 
        /// [1]=publicKey  公钥
        /// </summary>
        /// <returns></returns>
        public static List<string> GetKey()
        {
            return RsaKeyGenerator.Pkcs1Key(2048, true);
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="encoding">编码类型</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        public RSACrypt(string privateKey, string publicKey)
        {
            _encoding = Encoding.UTF8; 
             _RsaUtil = new RsaPkcs1Util(_encoding, publicKey, privateKey,1024);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="code">加密代码</param>
        /// <returns></returns>
        public string Encrypt(string code)
        {
            return _RsaUtil.Encrypt(code, RSAEncryptionPadding.Pkcs1);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="code">解密代码</param>
        /// <returns></returns>
        public string Decrypt(string code)
        {
            return _RsaUtil.Decrypt(code, RSAEncryptionPadding.Pkcs1);
        }

    }
}

using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace FytSoa.Common
{
    /// <summary>
    /// AES加密算法
    /// --敏感信息加密使用（动态密钥）
    /// --需实例化加密解密类
    /// </summary>
    public class AESEncrypt {

       public const string RetError = "x07x07x07x07x07";
       private readonly byte[] _iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
       private byte[] _key = { 0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76, 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0, 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, 0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75, 0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84, 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8, 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2, 0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73, 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb, 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, 0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08, 0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, 0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e, 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf, 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 };
        private const int CryptoKeyLength = 32;

        private AesCryptoServiceProvider _mAesCryptoServiceProvider;
        private string _mMessage;
       public string Message
       {
           get { return _mMessage; }
           set { _mMessage = value; }
       }
       private bool _mContainKey;
       /// <summary>
       /// True：密文中包含密钥
       /// False：密文中不包含密钥
       /// </summary>
       public bool ContainKey
       {
           get { return _mContainKey; }
           set { _mContainKey = value; }
       }
       public AESEncrypt()
       {
           _mAesCryptoServiceProvider = new AesCryptoServiceProvider();
           _mContainKey = true;
           _mMessage = string.Empty;
       }
       public AESEncrypt(bool containKey)
           : this()
       {
           _mContainKey = containKey;
       }
       private string Encrypt(string sCrypto, byte[] key)
       {
           string sEncryped = string.Empty;
           byte[] crypto, encrypted;
           ICryptoTransform ct;
  
           try
           {
               crypto = string2Byte(sCrypto);
               _mAesCryptoServiceProvider.Key = key;
               _mAesCryptoServiceProvider.IV = _iv;
               ct = _mAesCryptoServiceProvider.CreateEncryptor();
               encrypted = ct.TransformFinalBlock(crypto, 0, crypto.Length);
               if (_mContainKey)
               {
                   sEncryped += byte2HexString(key);
               }
               sEncryped += byte2HexString(encrypted);
               return sEncryped;
           }
           catch (Exception ex)
           {
               _mMessage = ex.ToString();
               return RetError;
           }
       }
       /// <summary>
       /// 指定密钥对明文进行AES加密
       /// </summary>
       /// <param name="sCrypto">明文</param>
       /// <param name="sKey">加密密钥</param>
       /// <returns></returns>
       public string Encrypt(string sCrypto, string sKey)
       {
           byte[] key = new byte[CryptoKeyLength];
  
           byte[] temp = string2Byte(sKey);
           if (temp.Length > key.Length)
           {
               _mMessage = "Key too long,need less than 32 Bytes key.";
               return RetError;
           }
           key = string2Byte(sKey.PadRight(key.Length));
           return Encrypt(sCrypto, key);
       }
       /// <summary>
       /// 动态生成密钥，并对明文进行AES加密
       /// </summary>
       /// <param name="sCrypto">明文</param>
       /// <returns></returns>
       public string Encrypt(string sCrypto)
       {
           byte[] key = new byte[CryptoKeyLength];
  
           _mAesCryptoServiceProvider.GenerateKey();
           key = _mAesCryptoServiceProvider.Key;
           return Encrypt(sCrypto, key);
       }

       private string Decrypt(string sEncrypted, byte[] key)
       {
           string s_decrypted = string.Empty;
           byte[] encrypted, decrypted;
           ICryptoTransform ct;
  
           try
           {
               encrypted = hexString2Byte(sEncrypted);
               _mAesCryptoServiceProvider.Key = key;
               _mAesCryptoServiceProvider.IV = _iv;
               ct = _mAesCryptoServiceProvider.CreateDecryptor();
               decrypted = ct.TransformFinalBlock(encrypted, 0, encrypted.Length);
               s_decrypted += byte2String(decrypted);
               return s_decrypted;
           }
           catch (Exception ex)
           {
               _mMessage = ex.ToString();
               _mMessage = "Decrypt fail.";
               return RetError;
           }
       }
       /// <summary>
       /// 从密文中解析出密钥，并对密文进行解密
       /// </summary>
       /// <param name="sEncrypted">密文</param>
       /// <returns></returns>
       public string Decrypt(string sEncrypted)
       {
           string s_key = string.Empty;
           byte[] key = new byte[CryptoKeyLength];
  
           if (sEncrypted.Length <= CryptoKeyLength * 2)
           {
               _mMessage = "Encrypted string invalid.";
               return RetError;
           }
           if (_mContainKey)
           {
               s_key = sEncrypted.Substring(0, CryptoKeyLength * 2);
               sEncrypted = sEncrypted.Substring(CryptoKeyLength * 2);
           }
           key = hexString2Byte(s_key);
           return Decrypt(sEncrypted, key);
       }
       /// <summary>
       /// 指定密钥，并对密文进行解密
       /// </summary>
       /// <param name="sEncrypted">密文</param>
       /// <param name="sKey">密钥</param>
       /// <returns></returns>
       public string Decrypt(string sEncrypted, string sKey)
       {
           byte[] key = new byte[CryptoKeyLength];
  
           byte[] temp = string2Byte(sKey);
           if (temp.Length > key.Length)
           {
               _mMessage = "Key invalid.too long,need less than 32 Bytes";
               return RetError;
           }
           key = string2Byte(sKey.PadRight(key.Length));
           if (_mContainKey)
           {
               sEncrypted = sEncrypted.Substring(CryptoKeyLength * 2);
           }
           return Decrypt(sEncrypted, key);
       }

       #region 私有方法
       private string byte2HexString(byte[] bytes)
       {
           StringBuilder sb = new StringBuilder();
           foreach (byte b in bytes)
           {
               sb.AppendFormat("{0:X2}", b);
           }
           return sb.ToString();
       }
       private byte[] hexString2Byte(string hex)
       {
           int len = hex.Length / 2;
           byte[] bytes = new byte[len];
           for (int i = 0; i < len; i++)
           {
               bytes[i] = (byte)(Convert.ToInt32(hex.Substring(i * 2, 2), 16));
           }
           return bytes;
       }
       private byte[] string2Byte(string str)
       {
           return Encoding.UTF8.GetBytes(str);
       }
       private string byte2String(byte[] bytes)
       {
           return Encoding.UTF8.GetString(bytes);
       }
       #endregion

    }


    /// <summary>
    ///3DES加密解密程序
    /// --备用
    /// </summary>
    public class DES3Encrypt
    {

        //密钥

        private static string sKey = "qJzGEh6hESZDVJeCnFPGuxzaiFYTLQM3";

        //矢量，矢量可以为空

        private static string sIV = "qcDY6X+aPLw=";

        //构造一个对称算法

        private static SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();



        public DES3Encrypt() { }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Value">明文</param>
        /// <returns>加密后的密文</returns>
        public static string EncryptString(string Value)
        {
            try
            {

                ICryptoTransform ct;

                MemoryStream ms;

                CryptoStream cs;

                byte[] byt;

                mCSP.Key = Convert.FromBase64String(sKey);

                mCSP.IV = Convert.FromBase64String(sIV);

                //指定加密的运算模式

                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;

                //获取或设置加密算法的填充模式

                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);

                byt = Encoding.UTF8.GetBytes(Value + "_0212YUAN");

                ms = new MemoryStream();

                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);

                cs.Write(byt, 0, byt.Length);

                cs.FlushFinalBlock();

                cs.Close();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return Value;
            }

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string DecryptString(string Value)
        {
            try
            {

                ICryptoTransform ct;

                MemoryStream ms;

                CryptoStream cs;

                byte[] byt;

                mCSP.Key = Convert.FromBase64String(sKey);

                mCSP.IV = Convert.FromBase64String(sIV);

                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;

                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);

                byt = Convert.FromBase64String(Value);

                ms = new MemoryStream();

                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);

                cs.Write(byt, 0, byt.Length);

                cs.FlushFinalBlock();

                cs.Close();

                return Encoding.UTF8.GetString(ms.ToArray()).Remove(Encoding.UTF8.GetString(ms.ToArray()).Length - 9, 9);
            }
            catch (Exception ex)
            {
                return Value;
            }

        }

    }
}

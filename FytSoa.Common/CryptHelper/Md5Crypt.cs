using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Common
{
    public class Md5Crypt
    {
        #region MD5加密字符串处理
        /// <summary>
        /// MD5加密字符串处理
        /// </summary>
        /// <param name="half">加密是16位还是32位；如果为true为16位</param>
        /// <param name="input">待加密码字符串</param>
        /// <returns></returns>
        public static string Encrypt(string input, bool half)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                strResult = strResult.Replace("-", "");
                if (half)//16位MD5加密（取32位加密的9~25字符）
                {
                    strResult = strResult?.Substring(8, 16);
                }
                return strResult;
            }            
        }
        #endregion

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strPwd">加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt(string strPwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(strPwd);
            byte[] result = md5.ComputeHash(data);
            string ret = "";
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
    }
}

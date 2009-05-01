using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;

namespace System.Web.Mvc {
    public static class GravatarHelper {

        public static string GetGravatar(this ViewPage page, string email) {
            string result = "<img src=\"{0}\" alt=\"Gravatar\" class=\"gravatar\" />";
            string url = GetGravatarURL(email, 48);
            return string.Format(result, url);
        }

        static string GetGravatarURL(string email, int size) {
            return (string.Format("http://www.gravatar.com/avatar/{0}?s={1}&r=PG", EncryptMD5(email), size.ToString()));
        }

        static string GetGravatarURL(string email, int size, string defaultImagePath) {
            return GetGravatarURL(email, size) + string.Format("&default={0}", defaultImagePath);
        }

        static string EncryptMD5(string Value) {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] valueArray = System.Text.Encoding.ASCII.GetBytes(Value);
            valueArray = md5.ComputeHash(valueArray);
            string encrypted = "";
            for (int i = 0; i < valueArray.Length; i++)
                encrypted += valueArray[i].ToString("x2").ToLower();
            return encrypted;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// use for Encrypt and Decrypt function
using System.Security.Cryptography;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Converters;

namespace ITOAPP_API.Helper
{
    public class CoreFunctions
    {
        private static IConfiguration configuration;
        private static IHttpContextAccessor ContextEnv;
        private static IWebHostEnvironment Environment;
        // This mothod will be call in contructor of Controler
        public static void SetEnv(IConfiguration _config, IHttpContextAccessor accessor, IWebHostEnvironment _environment)
        {
            configuration = _config;
            ContextEnv = accessor;
            Environment = _environment;
        }
        //static CoreFunctions()
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        //    configuration = builder.Build();

        //}
        public static string GetSector(String SectorName)
        {
            return configuration[SectorName];
        }

        public static string GetClientIP()
        {
            return ContextEnv.HttpContext.Connection.RemoteIpAddress.ToString();
        }
        public static string GetClientHostname()
        {
            try
            {
                return System.Net.Dns.GetHostEntry(GetClientIP()).HostName;
            }
            catch (Exception)
            {
                return GetClientIP();
            }
        }
        public static string Encryption(String EncryptValue)
        {
            string EncryptionKey = "MAKV2SPBNI99212$@%#";
            byte[] clearBytes = Encoding.Unicode.GetBytes(EncryptValue);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    EncryptValue = Convert.ToBase64String(ms.ToArray());
                }
            }
            return EncryptValue;
        }
        public static string Decryption(String DecryptValue)
        {
            string EncryptionKey = "MAKV2SPBNI99212$@%#";
            byte[] cipherBytes = Convert.FromBase64String(DecryptValue);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    DecryptValue = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return DecryptValue;
        }
        public static string DBdecrytionKey()
        {
            return "JNGHKln0ZHRYN/rCW4FmcikygpQC4b8dhhKbn2pwkmZvf94+bV9PxMpWA+7VqMBlpj9Yg4MZZmdqGAI90Zj58A==";
        }
        public static Boolean LDapAuthentication(string LogonUser, string LogonPassword)
        {
            bool boolStat;
            string Domain = Decryption(GetSector("LDapDomain"));
            try
            {
                System.DirectoryServices.DirectoryEntry AD = new System.DirectoryServices.DirectoryEntry(Domain, LogonUser, LogonPassword, System.DirectoryServices.AuthenticationTypes.Secure | System.DirectoryServices.AuthenticationTypes.Sealing | System.DirectoryServices.AuthenticationTypes.Signing);
                AD.RefreshCache();
                boolStat = true;
            }
            catch (Exception)
            {
                boolStat = false;
            }
            return boolStat;
        }
        public static ArrayList AuthenticateKeys()
        {
            ArrayList Keys = new ArrayList();
            var ParentNode = configuration.GetSection("Client_Apps:App");
            //var tmp_key;
            foreach (IConfigurationSection section in ParentNode.GetChildren())
            {
                var tmp_key = section.GetValue<string>("Key");
                Keys.Add(tmp_key);
            }
            return Keys;
        }
        
        //public static string CreateToken(ClaimModel Cm)
        //{
        //    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetSector("Jwt:Key")));
        //    var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        //    var Claims = new[]
        //    {
        //        new Claim("UserID",Cm.UserID),
        //        new Claim("Fullname", Cm.FullName)
        //    };
        //    string Issuer = GetSector("Jwt:Issuer");
        //    var TokenKeys = new JwtSecurityToken(GetSector("Jwt:Issuer"),
        //        GetSector("Jwt:Audience"),
        //        Claims,
        //        expires: DateTime.Now.AddMinutes(10),
        //        signingCredentials: Credentials);
        //    string ReturnToken = new JwtSecurityTokenHandler().WriteToken(TokenKeys);
        //    return ReturnToken;
        //}
        public static string FetchClaim(string ClaimID)
        {
            var value = ((ClaimsIdentity)ContextEnv.HttpContext.User.Identity).FindFirst(ClaimID).Value;

            return value;
        }
        public static ArrayList ToArrayList(string str_value)
        {
            ArrayList MyArrayList = new ArrayList();
            string[] TmpArray = str_value.Split(",");
            foreach (var I in TmpArray)
                MyArrayList.Add(I);
            return MyArrayList;
        }
        public static Boolean SearchArrayValue(ArrayList _array, string search_value, ref int index)
        {
            Boolean boolStat = false;

            for (var i = 0; i < _array.Count; i++)
            {
                if (search_value == _array[i].ToString())
                {
                    index = i;
                    boolStat = true;
                    break;
                }
            }
            return boolStat;
        }
        public static string GetAppContentDirectory()
        {
            string contentPath = Environment.ContentRootPath;
            return contentPath;
        }
        public static string ClientDecrypt(string _encrypt_value, string _encrypt_mode = "N")
        {
            string DecryptValue = "";
            if (_encrypt_mode == "N")
            {
                try
                {
                    _encrypt_mode = FetchClaim("EncryptMode");
                }
                catch (Exception)
                {

                    _encrypt_mode = "N";
                }
            }
            if (_encrypt_mode=="Y")
            {
                if (_encrypt_value=="")
                {
                    DecryptValue = "";
                    return DecryptValue;
                }
                byte[] keybytes = Encoding.UTF8.GetBytes(GetSector("Request:Encryption_Key"));
                byte[] iv = Encoding.UTF8.GetBytes(GetSector("Request:Victor_Key"));
                byte[] Encrypted = Convert.FromBase64String(_encrypt_value);
                using (var rijAlg = new RijndaelManaged())
                {
                    rijAlg.Mode = CipherMode.CBC;
                    rijAlg.Padding = PaddingMode.PKCS7;
                    rijAlg.FeedbackSize = 256 / 32;
                    rijAlg.Key = keybytes;
                    rijAlg.IV = iv;
                    // Create a decrytor to perform the stream transform.  
                    var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                    try
                    {
                        // Create the streams used for decryption.  
                        using (var msDecrypt = new MemoryStream(Encrypted))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {

                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    // Read the decrypted bytes from the decrypting stream  
                                    // and place them in a string.  
                                    DecryptValue = srDecrypt.ReadToEnd();

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                };
            }
            else
            {
                DecryptValue = _encrypt_value;
            }
            return DecryptValue;
        }
        
        public static void WriteLog(Exception _log, string _userid = "system")
        {
            StackFrame callStack = new StackFrame(1, true);
            string shortMsg = _log.Message.ToString();
            string detailMsg = _log.StackTrace.ToString();
            string logPath = GetAppContentDirectory().ToString() + "\\logs\\";
            int fileExist = Directory.GetFiles(logPath, _userid + ".log", SearchOption.TopDirectoryOnly).Count();

            if (fileExist == 0)
            {
                using (StreamWriter writer = new StreamWriter(logPath + _userid + ".log"))
                {
                    writer.WriteLine("[" + DateTime.Now.ToString() + "][" + Path.GetFileName(callStack.GetFileName().ToString()) + "][" + callStack.GetFileLineNumber().ToString() + "] " + shortMsg);
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(logPath + _userid + ".log", true))
                {
                    writer.WriteLine("[" + DateTime.Now.ToString() + "][" + Path.GetFileName(callStack.GetFileName().ToString()) + "][" + callStack.GetFileLineNumber().ToString() + "] " + shortMsg);
                }
            };
        }
        public static void WriteLog(string _log, string _userid = "system")
        {
            StackFrame callStack = new StackFrame(1, true);
            string detailMsg = _log.ToString();
            string logPath = GetAppContentDirectory().ToString() + "\\logs\\";
            int fileExist = Directory.GetFiles(logPath, _userid + ".log", SearchOption.TopDirectoryOnly).Count();

            if (fileExist == 0)
            {
                using (StreamWriter writer = new StreamWriter(logPath + _userid + ".log"))
                {
                    writer.WriteLine("[" + DateTime.Now.ToString() + "][" + Path.GetFileName(callStack.GetFileName().ToString()) + "][" + callStack.GetFileLineNumber().ToString() + "] " + detailMsg);
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(logPath + _userid + ".log", true))
                {

                    writer.WriteLine("[" + DateTime.Now.ToString() + "][" + Path.GetFileName(callStack.GetFileName().ToString()) + "][" + callStack.GetFileLineNumber().ToString() + "] " + detailMsg);
                }
            };
        }

        public static string GetContextValue(string item)
        {
            return ContextEnv.HttpContext.Items[item].ToString();
        }
        public static dynamic ToDynamic(string content)
        {
            return JsonConvert.DeserializeObject<ExpandoObject>(content, new ExpandoObjectConverter());
        }
    }
}

using CClientCrypt;
using ITOAPP_API.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StandardCrypt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CoreFunction;
using System.Threading;

namespace ITOAPP_API.Helper
{
    public class ContextHandler
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _clientFactory;
        public ContextHandler(RequestDelegate next, 
            IConfiguration _config, 
            IHttpContextAccessor httpContextAccessor, 
            IWebHostEnvironment _environment, 
            IHttpClientFactory clientFactory)
        {
            _next = next;
            _clientFactory = clientFactory;
            Core.Initiate(_config, httpContextAccessor, _environment);

        }
        public async Task Invoke(HttpContext context)
        {
            
            context.Request.EnableBuffering();
            context.Items["user_id"] = "SYSTEM";
            context.Items["real_debug"] = "Y";
            context.Items["user_debug"] = "Y";
            context.Items["verify_tokent"] = "failed";
            var request = context.Request;
            var end_point = request.GetEncodedUrl();
            int end_point_length = end_point.Length;
            int position_last_slash = end_point.LastIndexOf("/")+1;
            string opt = end_point.Substring(position_last_slash, end_point_length - position_last_slash);
            context.Items["debug_file_name"] = "SYSTEM_" + opt;
            ClBasicResponse Response = new ClBasicResponse();
            UserInfo UI = new UserInfo();
            TokenInfo CU = new TokenInfo();
            Core.DebugInfo("End Point: " + end_point);
            if (end_point.Contains("swagger"))
            {
                await _next.Invoke(context);
            }
            if (end_point.Contains("swagger") == false)
            {
                try
                {
                    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(); // Bear token
                                                                                                             
                    if (token != null)
                    {
                        try
                        {
                            string key = Core.GetAppSetting("Key:Standard").ToString();
                            CU = JsonConvert.DeserializeObject<TokenInfo>(SCrypt.Decrypt(key, token.ToString()));
                        }
                        catch (Exception ex)
                        {
                            Core.DebugInfo("Error: Invalid Token Key");
                            Core.DebugError(ex);

                            Response.status = "-1";
                            Response.message = "Invalid Token Key";
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            var jsonObject = JsonConvert.SerializeObject(Response);
                            await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
                            return;
                        }

                        try
                        {
                            string userInfoString = await GetUserInfo(CU.url_get_user_info, token, end_point);
                            UI = JsonConvert.DeserializeObject<UserInfo>(SCrypt.Decrypt(Core.GetAppSetting("Key:Standard"), userInfoString));
                            context.Items["project"] = CU.project;
                            context.Items["user_id"] = CU.auth_user;
                            context.Items["username"] = UI.username;
                            context.Items["bank_code"] = UI.bank_code;
                            context.Items["user_email"] = UI.user_email;
                            context.Items["bank_email"] = UI.bank_email;
                            context.Items["role_id"] = UI.role_id;
                            context.Items["real_debug"] = UI.real_debug;
                            context.Items["user_debug"] = UI.user_debug;
                            context.Items["debug_file_name"] = CU.auth_user+"_"+ opt;
                            if (UI.issuer == "N/A" || UI.audience == "N/A" || UI.jwt_key == "N/A")
                            {
                                Core.DebugInfo("Failed to get user information");
                                Core.DebugInfo("Issuer: " + UI.issuer);
                                Core.DebugInfo("Audience: " + UI.audience);
                                Core.DebugInfo("Jwt Key: " + UI.jwt_key);
                                Core.DebugInfo("Failed to get user information");

                                Response.status = "-1";
                                Response.message = "Failed to get user information";
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                var jsonObject = JsonConvert.SerializeObject(Response);
                                await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
                                return;
                            }

                            try
                            {
                                Core.DebugInfo("Start verify token key...");
                                
                                Response = await VerifyToken(CU.url_verify_token, CU.token, UI.issuer, UI.audience, UI.jwt_key);
                                if (Response.status == "-1")
                                {
                                    Core.DebugInfo("Invalid Token Key or Expired");
                                    Response.status = "-1";
                                    Response.message = "Invalid Token or Expired";
                                    context.Response.ContentType = "application/json";
                                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                                    var jsonObject = JsonConvert.SerializeObject(Response);
                                    await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
                                    return;
                                }
                                else
                                {
                                    Core.DebugInfo("Token Key is valid");

                                    context.Items["verify_tokent"] = "Token is valid";
                                    try
                                    {
                                        Core.DebugInfo("Start fetching connection string...");
                                        
                                        List<ClConnectionList> L_CONN = await GetConnection(CU.url_get_connection, token, CU.project);

                                        Core.DebugInfo("Total connections fetched: " + L_CONN.Count().ToString());

                                        for (var i = 0; i < L_CONN.Count(); i++)
                                        {
                                            Core.DebugInfo("Connection " + (i + 1).ToString() + ": " + L_CONN[i].CONN_NAME);

                                            context.Items[L_CONN[i].CONN_NAME] = L_CONN[i].CONN_STR;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Core.DebugInfo("Failed to get connection string");
                                        Core.DebugError(ex);

                                        Response.status = "-1";
                                        Response.message = "Failed to get connection string";
                                        context.Response.ContentType = "application/json";
                                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                                        var jsonObject = JsonConvert.SerializeObject(Response);
                                        await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
                                        return;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Core.DebugInfo("Failed to verify Token Key");
                                Core.DebugError(ex);

                                Response.status = "-1";
                                Response.message = "Failed to verify Token Key";
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                var jsonObject = JsonConvert.SerializeObject(Response);
                                await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            Core.DebugInfo("Failed to get user information");
                            Core.DebugError(ex);

                            Response.status = "-1";
                            Response.message = "Failed to get user information";
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            var jsonObject = JsonConvert.SerializeObject(Response);
                            await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
                            return;
                        }
                    }
                    else
                    {
                        Core.DebugInfo("Token Key is null");

                        UI.required_encrypt = "N";
                    }

                    Core.DebugInfo("Content reqired encryption: " + UI.required_encrypt);

                    if (UI.is_end_point == "INVALID" && token != null)
                    {
                        Core.DebugInfo("Invalid End Point URL");

                        Response.status = "-1";
                        Response.message = "Invalid End Point URL";
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        var jsonObject = JsonConvert.SerializeObject(Response);
                        await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
                        return;
                    }
                    var stream = request.Body;// currently holds the original stream                    
                    var originalContent = new StreamReader(stream).ReadToEndAsync();

                    

                    string newRequest = null;
                    var requestData = (byte[]?)null;

                    if (UI.required_encrypt == "Y")
                    {
                        try
                        {
                            Core.DebugInfo("Start decrypt content body...");

                            newRequest = Client.Decrypt(originalContent.Result, UI.ekey, UI.eiv);

                            Core.DebugInfo("Successfully decrypted...");
                        }
                        catch (Exception ex)
                        {
                            Core.DebugInfo("Failed to decrypte content body");
                            Core.DebugError(ex);

                            Response.status = "-1";
                            Response.message = "Request Data Decryption Failed";
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            var jsonObject = JsonConvert.SerializeObject(Response);
                            await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
                            return;
                        }

                        requestData = Encoding.UTF8.GetBytes(newRequest);
                    }
                    else
                    {
                        requestData = Encoding.UTF8.GetBytes(originalContent.Result);
                    };
                    stream = new MemoryStream(requestData);
                    request.Body = stream;
                    await _next.Invoke(context);
                    stream.Close();
                    stream.Dispose();
                    originalContent.Dispose();
                }
                catch (Exception main_ex)
                {
                    Core.DebugError(main_ex);
                }
                
            }

        }
        public async Task<ClBasicResponse> VerifyToken(string middleware_end_point, string token, string issuer, string audiece, string key)
        {
            ClBasicResponse BR = new ClBasicResponse();
            HttpResponseMessage respond = new HttpResponseMessage();
            
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            HttpClient _client = new HttpClient(handler);
            try
            {

                ClVerifyToken VT = new ClVerifyToken();
                VT.token_key = token;
                VT.issuer = issuer;
                VT.audience = audiece;
                VT.key = key;
                var json_request = JsonConvert.SerializeObject(VT);
                string keyEncrypt = Core.GetAppSetting("Key:Standard");
                var conten = SCrypt.Encrypt(keyEncrypt, json_request);
                Core.DebugInfo("Token for verify: " + conten.ToString());
                var stringContent = new StringContent("\"" + conten + "\"", Encoding.UTF8, "application/json");


                _client.BaseAddress = new Uri(middleware_end_point);
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                respond = await _client.PostAsync("", stringContent);
                Core.DebugInfo("Verify repond code: " + respond.StatusCode.ToString());
                if (respond.IsSuccessStatusCode)
                {
                    var customerJsonString = await respond.Content.ReadAsStringAsync();
                    BR = JsonConvert.DeserializeObject<ClBasicResponse>(customerJsonString);
                }
                else
                {
                    var customerJsonString = await respond.Content.ReadAsStringAsync();
                    Core.DebugInfo("Error: unsuccess calling middleware for verify token");
                    Core.DebugInfo("Response: " + customerJsonString.ToString());

                    BR.status = "-1";
                    BR.message = "Unsuccess calling middleware for verify token";
                }
               


            }
            catch (Exception ex)
            {
                _client.Dispose();
                respond.Dispose();
                handler.Dispose();
                Core.DebugInfo("Failed function VerifyToken");
                Core.DebugError(ex);
                BR.status = "-1";
                BR.message = "Failed function VerifyToken";
            }
            finally
            {
                _client.Dispose();
                handler.Dispose();
                respond.Dispose();
            }

            return await Task.FromResult<ClBasicResponse>(BR);
        }
        public async Task<string> GetUserInfo(string middleware_end_point, string full_token, string end_point)
        {
            HttpResponseMessage respond = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            HttpClient _client = new HttpClient(handler);
            string UserInfoString = "";
            try
            {
                ClGetInfo CI = new ClGetInfo();
                CI.token_key = full_token;
                CI.end_point = end_point;
                var json_request = JsonConvert.SerializeObject(CI);
                string key = Core.GetAppSetting("Key:Standard");
                var conten = SCrypt.Encrypt(key, json_request);
                var stringContent = new StringContent("\"" + conten + "\"", Encoding.UTF8, "application/json");

                

                _client.BaseAddress = new Uri(middleware_end_point);
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                respond = await _client.PostAsync("", stringContent);
                if (respond.IsSuccessStatusCode)
                {
                    var customerJsonString = await respond.Content.ReadAsStringAsync();
                    UserInfoString = JsonConvert.DeserializeObject<string>(customerJsonString);

                }
                else
                {
                    var customerJsonString = await respond.Content.ReadAsStringAsync();

                    Core.DebugInfo("Unsuccess calling middleware for getting user information");
                    Core.DebugInfo("Response: " + customerJsonString.ToString());
                }
                
            }
            catch (Exception ex)
            {
                _client.Dispose();
                respond.Dispose();
                handler.Dispose();
                Core.DebugInfo("Failed function GetUserInfo");
                Core.DebugError(ex);
            }
            finally
            {
                _client.Dispose();
                handler.Dispose();
                respond.Dispose();
            }


            return await Task.FromResult<string>(UserInfoString);
        }
        public async Task<List<ClConnectionList>> GetConnection(string middleware_end_point, string full_token, string project)
        {
            HttpResponseMessage respond = new HttpResponseMessage();
         
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            HttpClient _client = new HttpClient(handler);
            List<ClConnectionList> L_CONN = new List<ClConnectionList>();
            try
            {
                var stringContent = new StringContent("\"" + project + "\"", Encoding.UTF8, "application/json");

                _client.BaseAddress = new Uri(middleware_end_point);
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + full_token);
                respond = await _client.PostAsync("", stringContent);
                if (respond.IsSuccessStatusCode)
                {
                    var customerJsonString = await respond.Content.ReadAsStringAsync();
                    var L_CONN1 = JsonConvert.DeserializeObject<string>(customerJsonString);
                    L_CONN = JsonConvert.DeserializeObject<List<ClConnectionList>>(L_CONN1);

                }
                else
                {
                    var customerJsonString = await respond.Content.ReadAsStringAsync();
                    Core.DebugInfo("Unsuccess calling middleware for getting connection string");
                    Core.DebugInfo("Response: " + customerJsonString.ToString());
                }
                
            }
            catch (Exception ex)
            {
                _client.Dispose();
                handler.Dispose();
                respond.Dispose();
                Core.DebugInfo("Failed function GetConnection");
                Core.DebugError(ex);
            }
            finally
            {
                _client.Dispose();
                handler.Dispose();
                respond.Dispose();
            }


            return await Task.FromResult<List<ClConnectionList>>(L_CONN);
        }
    }
    public class ClGetInfo
    {
        public string token_key { get; set; }
        public string end_point { get; set; }
    }
    public class ClVerifyToken
    {
        public string token_key { get; set; }
        public string issuer { get; set; }
        public string audience { get; set; }
        public string key { get; set; }
    }
    public class TokenInfo
    {
        public string token { get; set; }
        public string auth_user { get; set; }
        public string project { get; set; }
        public string url_get_user_info { get; set; }
        public string url_verify_token { get; set; }
        public string url_get_connection { get; set; }
    }
    public class UserInfo
    {
        public string jwt_key { get; set; }
        public string issuer { get; set; }
        public string audience { get; set; }
        public string auth_key { get; set; }
        public string ekey { get; set; }
        public string eiv { get; set; }
        public string is_end_point { get; set; }
        public string required_encrypt { get; set; }
        public string username { get; set; }
        public string bank_code { get; set; }
        public string user_email { get; set; }
        public string bank_email { get; set; }
        public string role_id { get; set; }
        public string auth_encrypt { get; set; }
        public string real_debug { get; set; }
        public string user_debug { get; set; }
    }
    public class ClBasicResponse
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class ClConnectionList
    {
        public string CONN_NAME { get; set; }
        public string CONN_STR { get; set; }
    }
}

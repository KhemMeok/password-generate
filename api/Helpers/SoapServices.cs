using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace ITOAPP_API.Helpers
{
    public class SoapServices
    {
        public static HttpWebRequest CreateRequest(string end_point)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(end_point);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\";action=\"SOAP:Action\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
        public static string SendRequest(string end_point, string request_body)
        {
            string result;
            try
            {
                HttpWebRequest request = CreateRequest(end_point);
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(request_body);
                Stream stream = request.GetRequestStream();
                soapEnvelopeXml.Save(stream);
                stream.Dispose();
                stream.Close();
                WebResponse response = request.GetResponse();
                StreamReader rd = new StreamReader(response.GetResponseStream());
                result = rd.ReadToEnd();
                response.Dispose();
                response.Close();
                rd.Dispose();
                rd.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}

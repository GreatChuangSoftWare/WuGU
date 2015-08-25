using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace SoftPlatform.Ashx
{
    /// <summary>
    /// WebChatHander 的摘要说明
    /// </summary>
    public class WebChatHander : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            context.Response.ContentType = "text/plain";
            string postString = string.Empty;
            if (context.Request.HttpMethod.ToUpper() == "POST")
            {
                using (Stream stream = context.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);

                    ///////////
                    var path = context.Server.MapPath("~/webchatlog.txt");
                    var sr = System.IO.File.AppendText(path);
                    sr.WriteLine(postString);
                    sr.Close();
                    //////////////////

                    var text = Handle(postString);
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.Write(text);
                }
            }
            else
            {
                string token = "hqc";
                if (string.IsNullOrEmpty(token))
                {
                    context.Response.Write("");
                }

                string echoString = context.Request.QueryString["echoStr"];
                string signature = context.Request.QueryString["signature"];
                string timestamp = context.Request.QueryString["timestamp"];
                string nonce = context.Request.QueryString["nonce"];

                var path = context.Server.MapPath("~/webchatlog.txt");
                var sr = System.IO.File.AppendText(path);
                sr.WriteLine(echoString);
                sr.Close();
                //return echoString;
                context.Response.Write(echoString);
            }
        }

        public string Handle(string postString)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(postString);
            XmlElement rootElement = doc.DocumentElement;
            XmlNode MsgType = rootElement.SelectSingleNode("MsgType");
            //RequestXML requestXML = new RequestXML();

            var text = TextHandle(doc);
            //messageHelp help = new messageHelp();
            //string responseContent = help.ReturnMessage(postStr);

            //Response.ContentEncoding = Encoding.UTF8;
            //Response.Write(text);
            return text;
        }


        //接受文本消息
        public string TextHandle(XmlDocument xmldoc)
        {
            string responseContent = "";
            XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
            XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
            XmlNode Content = xmldoc.SelectSingleNode("/xml/Content");
            if (Content != null)
            {
                responseContent = string.Format(Message_Text,
                    FromUserName.InnerText,
                    ToUserName.InnerText,
                    DateTime.Now.Ticks,
                    "欢迎使用微信公共账号，您输入的内容为：" + Content.InnerText + "\r\n<a href=\"http://www.cnblogs.com\">点击进入</a>");
            }

            return responseContent;
        }

        /// <summary>
        /// 普通文本消息
        /// </summary>
        public static string Message_Text
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[text]]></MsgType>
                            <Content><![CDATA[{3}]]></Content>
                            </xml>";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
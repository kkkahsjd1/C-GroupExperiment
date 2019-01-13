using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMediaPlayer
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class MyMessages
    {
        public static MessageFlag Flag { get; set; }//获取当前状态，0为unpressed,1为pressed
        public string TimeUp { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public MyMessages(/*string user,*/ string messages)
        {
            this.Message = messages;
            //this.User = user;
            this.TimeUp = DateTime.Now.ToString();
        }
        public MyMessages()
        { }
    }
}

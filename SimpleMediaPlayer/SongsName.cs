using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleMediaPlayer
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class SongsName
    {
        public static List<SongsName> songs { get; set; }
        public string Song { get; set; }
        public List<MyMessages> messages { get; set; }
        public SongsName()
        {
            songs = new List<SongsName>();
            messages = new List<MyMessages>();
        }

        public static void Export(string FileName)
        {
            try
            {
                XmlSerializer xmlser = new XmlSerializer(typeof(List<SongsName>));
                FileStream fs = new System.IO.FileStream(FileName, FileMode.Create, FileAccess.ReadWrite);
                xmlser.Serialize(fs, songs);
                fs.Close();
                string xml = File.ReadAllText(FileName);
                Console.WriteLine(xml);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception:{e.ToString()}");
            }
        }
        public static void Import(string FileName)
        {
            try
            {
                Stream fStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                XmlSerializer xmlser = new XmlSerializer(typeof(List<SongsName>));
                songs = (List<SongsName>)xmlser.Deserialize(fStream);

       
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "   读取弹幕异常");
            }
        }

        public SongsName(string songname) : this()
        {
            this.Song = songname;
        }
    }
}

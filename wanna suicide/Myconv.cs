using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wanna_suicide
{
    internal class Myconv
    {
        private static string dekstop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static T MyDeserialize<T>(string FileName)
        {
                if(!File.Exists(dekstop + "\\" + FileName))
                {
                    List<Zametki> def1 = new List<Zametki>();
                    string def = JsonConvert.SerializeObject(def1);
                    File.WriteAllText(dekstop + "\\" + FileName, def);
                }
                string json = File.ReadAllText(dekstop + "\\" + FileName);
                T primer = JsonConvert.DeserializeObject<T>(json);
                return primer;
        }

        public static void Myserialize<T>(T dannie, string FileName)
        {
            string json = JsonConvert.SerializeObject(dannie);
            File.WriteAllText(dekstop + "\\" + FileName, json);
        }
    }
}

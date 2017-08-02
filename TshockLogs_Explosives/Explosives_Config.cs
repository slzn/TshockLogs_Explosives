using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TshockLogs
{
    class Explosives_Config
    {
        [JsonProperty]
        public bool ShowOnConsole = false;

        [JsonProperty]
        public string[] Explosives = { "Bomb", "BouncyBomb" , "StickyBomb",
                                        "Dynamite", "BouncyDynamite", "StickyDynamite",
                                        "BombFish", "Explosives", "ExplosiveBunny"};

        internal static Explosives_Config Read(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Using default configs");
                Explosives_Config config = new Explosives_Config();
                config.Write(path);
                return config;
            }
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Read(fs);
            }
        }

        internal static Explosives_Config Read(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<Explosives_Config>(sr.ReadToEnd());
            }
        }

        internal void Write(string path)
        {
            string dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                Write(fs);
            }
        }

        internal void Write(Stream stream)
        {
            var str = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (var sw = new StreamWriter(stream))
            {
                sw.Write(str);
            }
        }
    }
}

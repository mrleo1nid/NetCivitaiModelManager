using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public class ConfigService 
    {
        public Config Config { get; set; }

        private string _filename;
        public ConfigService(string filename)
        {
            _filename = filename;
            if (string.IsNullOrEmpty(_filename)) _filename = "config.json";
            LoadConfig();
        }
        public void LoadConfig()
        {
            try
            {
                if (!File.Exists(_filename))
                {
                    Config = new Config();
                    SaveConfig();
                }
                else
                {
                    var text = File.ReadAllText(_filename);
                    var conf = JsonSerializer.Deserialize<Config>(text);
                    if (conf != null)
                        Config = conf;
                    else
                        Config = new Config();
                }
            }
            catch  { }  
        }
        public void SaveConfig()
        {
            try
            {
                var text = JsonSerializer.Serialize(Config, new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(_filename, text);
            }
            catch { }
        }
    }
}

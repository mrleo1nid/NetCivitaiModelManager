using Downloader;
using NetCivitaiModelManager.Models;
using System.IO;
using System.Text.Json;

namespace NetCivitaiModelManager.Services
{
    public sealed class ConfigService
    {

        public Config Config { get; set; }
        public DownloadConfiguration DownloadConfiguration { get;set;} = new DownloadConfiguration() { ParallelDownload =true };

        private string _filename;

        public ConfigService(string filename)
        {
           _filename = filename;
           LoadConfig();
        }
        public  void LoadConfig()
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
        public  void SaveConfig()
        {
            var text = JsonSerializer.Serialize(Config, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(_filename, text);
        }
    }
}

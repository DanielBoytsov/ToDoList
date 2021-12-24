using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonSettings
{
    public class JsonSettingsManager
    {
        public string SettingsPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(),"ToDoListConfig.json");

        public JsonSettings CurrentSettings { get; }

        public JsonSettingsManager()
        {
            CurrentSettings = GetCurrentSettings(SettingsPath);
        }

        public JsonSettingsManager(string path)
        {
            CurrentSettings = GetCurrentSettings(path);
        }

        private JsonSettings GetCurrentSettings(string settingsPath)
        {
            try
            {
                var json = File.ReadAllText(settingsPath);

                return JsonSerializer.Deserialize<JsonSettings>(json);

            }
            catch (FileNotFoundException)
            {
                CreateSettingsFile(settingsPath);

                return new JsonSettings();
            }
            catch (Exception ex)
            {
                return new JsonSettings();
            }
        }

        private void CreateSettingsFile(string settingsPath)
        {
            try
            {
                var jsonSettings = new JsonSettings();

                var json = JsonSerializer.Serialize<JsonSettings>(jsonSettings);

                File.WriteAllText(settingsPath, json);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
            }
        }
    }
}

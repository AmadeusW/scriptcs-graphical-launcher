using ScriptCs.Launcher.Wpf.ScriptList;
using ScriptCs.Launcher.Wpf.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Launcher.Wpf.Helpers
{
    public static class FileHelpers
    {
        private const string appName = "ScriptLauncher";
        private const string configName = "config.yml";

        public static string GetConfigurationPath()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var scriptDataPath = Path.Combine(appData, appName, configName);
            return scriptDataPath;
        }

        public static SavedApplicationVM LoadConfiguration()
        {
            if (!File.Exists(GetConfigurationPath()))
                return null;

            using (var stream = File.OpenText(GetConfigurationPath()))
            {
                var d = new YamlDotNet.Serialization.Deserializer();
                var configuration = d.Deserialize<SavedApplicationVM>(stream);
                return configuration;
            }
        }

        public static void SaveConfiguration(SavedApplicationVM configuration)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(GetConfigurationPath()));
            using (var writer = File.CreateText(GetConfigurationPath()))
            {
                var s = new YamlDotNet.Serialization.Serializer();
                s.Serialize(writer, configuration);
            }
        }

    }
}

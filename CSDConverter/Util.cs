using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSDConverter
{
    class Util
    {
        private static String configFileName = "LocalConfig.json";

        public static void saveSrcPath(string srcPath)
        {
            EnsureLocalConfig();
        }

        private static String EnsureLocalConfig()
        {
            String appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSDConverter");
            if (!Directory.Exists(appDir))
                Directory.CreateDirectory(appDir);

            String configPath = Path.Combine(appDir, configFileName);
            if (!File.Exists(configPath))
                File.Create(configPath);

            return configPath;
        }
    }
}

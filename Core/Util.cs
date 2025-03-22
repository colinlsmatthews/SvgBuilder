using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgBuilder.Core
{
    public class Util
    {
        // UTILITY CLASS: private constructor to prevent instantiation
        private Util()
        {
        }

        public static string GetFilename(string path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }


        public static bool ValidatePath(string? path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return false;
            }
            return true;
        }

        public static bool ValidateDirectory(string? outputDir)
        {
            if (string.IsNullOrEmpty(outputDir) || !Directory.Exists(outputDir))
            {
                return false;
            }
            return true;
        }

        public static string ConstructOutputPath(string outputDir, string filename)
        {
            return Path.Combine(outputDir, filename + ".svg");
        }
    }
}

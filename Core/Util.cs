using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


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

        public static void CalculateBounds(List<SvgElement> elements, 
            out int width, 
            out int height,
            out int minX,
            out int minY)
        {
            minX = elements.Min(e => e.Min.Item1);
            minY = elements.Min(e => e.Min.Item2);
            int maxX = elements.Max(e => e.Max.Item1);
            int maxY = elements.Max(e => e.Max.Item2);

            width = maxX - minX;
            height = maxY - minY;
        }
    }
}

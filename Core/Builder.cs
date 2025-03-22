using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace SvgBuilder.Core
{
    public class Builder
    {
        public Builder()
        {
        }

        public bool Build(string? inputPath, string? outputDirectory, out string destinationPath, out Exception e)
        {
            string filename = String.Empty;
            destinationPath = String.Empty;
            e = new Exception();

            try
            {
                if (!Util.ValidatePath(inputPath))
                {
                    throw new Exception("Invalid input path.");
                }
                if (!Util.ValidateDirectory(outputDirectory))
                {
                    throw new Exception("Invalid output directory.");
                }
                destinationPath = Util.ConstructOutputPath(outputDirectory!, Util.GetFilename(inputPath!));
                // TODO: add logic to read json from inputPath
                // TODO: add logic to build svg from json
                // TODO: add logic to save svg to outputPath

            }
            catch (Exception ex)
            {
                e = ex;
                return false;
            }
            return true;
        }


        private string ReadJson(string inputPath)
        {
            JsonTextReader reader = new JsonTextReader(new StreamReader(inputPath));
            return reader.ReadAsString();
        }

        private string BuildSvg(string json)
        {
            return String.Empty;
        }

        private bool SaveSvg(string svg, string outputPath)
        {
            return true;
        }
    }
}

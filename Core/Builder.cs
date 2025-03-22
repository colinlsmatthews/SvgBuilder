using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SvgBuilder.Core
{
    public class Builder
    {
        public Builder()
        {
        }

        public bool Build(string? inputPath, string? outputPath, out string destination, out Exception e)
        {
            destination = @"test\path.svg";
            e = new Exception();

            try
            {
                if (!validatePath(inputPath))
                {
                    throw new Exception("Invalid input path.");
                }
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

        

        private bool validatePath(string? path)
        {
            return true;
        }

        private string ReadJson(string inputPath)
        {
            JsonTextReader reader = new JsonTextReader(new StreamReader(inputPath));
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

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace SvgBuilder.Core
{
    public class Builder
    {
        public Builder()
        {
            // TODO: implement singleton pattern for Builder
        }

        public bool Build(string? inputPath, string? outputDirectory, out string destinationPath, out Exception e)
        {
            string filename = string.Empty;
            destinationPath = string.Empty;
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
                string json = ReadJson(inputPath!);
                SvgSpec svgSpec = GetSvgSpec(json);
                bool svg = BuildSvg(svgSpec);

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
            string jsonStringFromFile = File.ReadAllText(inputPath);
            return jsonStringFromFile;
        }

        private SvgSpec GetSvgSpec(string jsonString)
        {
            // Establish method variables
            List<SvgElement> svgElements = new List<SvgElement>();
                        
            // Establish json load settings. Use defaults for now
            JsonLoadSettings settings = new JsonLoadSettings()
            {
                CommentHandling = CommentHandling.Ignore,
                DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace,
                LineInfoHandling = LineInfoHandling.Load
            };

            JObject jsonData = JObject.Parse(jsonString, settings);
            // TODO: add logic to allow for other shapes (i.e. circles, polygons, etc.)
            JArray boxesData = (JArray)jsonData["boxes"];

            // Assume all shapes are rectangles for now
            for(int i = 0; i < boxesData.Count; i++) 
            {
                JObject box = (JObject)boxesData[i];

                Tuple<int, int> min = new Tuple<int, int>((int)box["min"][0], (int)box["min"][1]);
                Tuple<int, int> max = new Tuple<int, int>((int)box["max"][0], (int)box["max"][1]);
                Color color = ColorTranslator.FromHtml((string)box["color"]);

                SvgElement element = new SvgElement($"box_{i}", min, max, color, "rectangle");
                svgElements.Add(element);
            }

            // Hardcode width, height, and bg color for now
            // TODO: add logic to allow for dynamic svg size and bg color
            SvgSpec svgSpec = new SvgSpec(1000, 1000, Color.White, svgElements);
            return svgSpec;

        }

        private bool BuildSvg(SvgSpec spec)
        {
            List<SvgElement> svgElements = spec.Elements;
            foreach (SvgElement svgElement in svgElements)
            {
                Console.WriteLine(svgElement.Id);
            }
            return true;
        }

        private bool SaveSvg(string svg, string outputPath)
        {
            return true;
        }
    }
}

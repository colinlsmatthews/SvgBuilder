using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                string svg = BuildSvgLINQ(svgSpec);
                Console.WriteLine(svg);

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
            
            // Pare the json string into a newtonsoft jobject
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

        // SVG builder using LINQ to XML
        private string BuildSvgLINQ(SvgSpec spec)
        {
            List<SvgElement> svgElements = spec.Elements;

            XNamespace svgNamespace = "http://www.w3.org/2000/svg";

            XElement outputSvg = new XElement(svgNamespace + "svg",
                new XAttribute("width", spec.Width),
                new XAttribute("height", spec.Height),
                new XAttribute("style", $"background-color: {ColorTranslator.ToHtml(spec.BackgroundColor)}"),

                // TODO: change logic to accomodate different shapes
                // TODO: IMPORTANT: Change logid to place shapes in center of display window
                // Create the shapes within the svg
                svgElements.Select(shape => new XElement(svgNamespace + "rect", 
                    new XAttribute("x", shape.Min.Item1),
                    new XAttribute("y", shape.Min.Item2),
                    new XAttribute("width", shape.Max.Item1 - shape.Min.Item1),
                    new XAttribute("height", shape.Max.Item2 - shape.Min.Item2),
                    new XAttribute("fill", ColorTranslator.ToHtml(shape.Color)
                    ))));
            return outputSvg.ToString();
        }

        private bool SaveSvg(string svg, string outputPath)
        {
            return true;
        }
    }
}

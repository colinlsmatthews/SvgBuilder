using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SvgBuilder.Core
{
    public class SvgSpec
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackgroundColor { get; set; }
        public List<SvgElement> Elements { get; set; }

        // Constructor to auto-calculate width and height
        public SvgSpec(Color backgroundColor, List<SvgElement> elements)
        {
            BackgroundColor = backgroundColor;
            Elements = elements;
            CalculateBounds(elements, out int width, out int height);
            Width = width;
            Height = height;
        }

        // Constructor for custom width and height
        public SvgSpec(int width, int height, Color backgroundColor, List<SvgElement> elements)
        {
            CalculateBounds(elements, out int calculatedWidth, out int calculatedHeight);
            if (calculatedWidth > width && calculatedHeight > height)
            {
                throw new ArgumentException($"Specified width and height are too small to encompass elements" +
                    $"\nWidth must be greater than {calculatedWidth}" +
                    $"\nHeight must be greater than {calculatedHeight}");
            }
            if (calculatedWidth > width)
            {
                throw new ArgumentException("Specified width is too small to encompass elements" +
                    $"\nWidth must be greater than {calculatedWidth}");
            }
            if (calculatedHeight > height)
            {
                throw new ArgumentException("Specified height is too small to encompass elements" +
                    $"\nHeight must be greater than {calculatedHeight}");
            }
            Width = width;
            Height = height;
            BackgroundColor = backgroundColor;
            Elements = elements;
        }

        private void CalculateBounds(List<SvgElement> elements, out int width, out int height)
        {
            int minX = elements.Min(e => e.Min.Item1);
            int minY = elements.Min(e => e.Min.Item2);
            int maxX = elements.Max(e => e.Max.Item1);
            int maxY = elements.Max(e => e.Max.Item2);

            width = maxX - minX;
            height = maxY - minY;
        }


    }
}

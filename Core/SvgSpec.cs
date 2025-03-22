using System;
using System.Collections.Generic;
using System.Drawing;

namespace SvgBuilder.Core
{
    public class SvgSpec
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double TranslationX { get; set; }
        public double TranslationY { get; set; }
        public Color BackgroundColor { get; set; }
        public List<SvgElement> Elements { get; set; }

        // Default constructor
        public SvgSpec()
        {
            Width = 0;
            Height = 0;
            TranslationX = 0;
            TranslationY = 0;
            BackgroundColor = Color.White;
            Elements = new List<SvgElement>();
        }

        // Override constructor to auto-calculate width and height
        public SvgSpec(Color backgroundColor, List<SvgElement> elements)
        {
            BackgroundColor = backgroundColor;
            Elements = elements;
            Util.CalculateBounds(elements, out int width, out int height, out int minX, out int minY);
            Width = width;
            Height = height;
            TranslationX = -minX;
            TranslationY = -minY;
        }

        // Override constructor for custom width and height
        public SvgSpec(int width, int height, Color backgroundColor, List<SvgElement> elements)
        {
            Util.CalculateBounds(elements, out int calculatedWidth, out int calculatedHeight, out int minX, out int minY);
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
            TranslationX = (width - calculatedWidth) / 2.0 - minX;
            TranslationY = (height - calculatedHeight) / 2.0 - minY;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgBuilder.Core
{
    public class SvgSpec
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackgroundColor { get; set; }
        public List<SvgElement> Elements { get; set; }


        public SvgSpec(int width, int height, Color backgroundColor, List<SvgElement> elements)
        {
            Width = width;
            Height = height;
            BackgroundColor = backgroundColor;
            Elements = elements;
        }


    }
}

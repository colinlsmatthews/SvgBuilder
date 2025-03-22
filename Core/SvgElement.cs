using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgBuilder.Core
{
    public struct SvgElement
    {
        public string? Id { get; set; }
        public Tuple<int, int> Min { get; set; }
        public Tuple<int, int> Max { get; set; }
        public Color Color { get; set; }
        public string Type { get; set; }

        public SvgElement(Tuple<int, int> min, Tuple<int, int> max, Color color, string type)
        {
            Id = null;
            Min = min;
            Max = max;
            Color = color;
            Type = type;
        }

        // Override to include unique id for elements
        public SvgElement(string id, Tuple<int, int> min, Tuple<int, int> max, Color color, string type)
        {
            Id = id;
            Min = min;
            Max = max;
            Color = color;
            Type = type;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Type: {Type}, Min: ({Min.Item1}, {Min.Item2}), Max: ({Max.Item1}, {Max.Item2}), Color: {Color}";
        }
    }
}

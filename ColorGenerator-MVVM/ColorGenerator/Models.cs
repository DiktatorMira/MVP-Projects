using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;

namespace ColorGenerator {
    class ColorModel {
        public string color_name { get; set; }
        public ColorModel(string color) => color_name = color;
    }
}

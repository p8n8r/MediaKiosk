using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MediaKiosk
{
    public class Types
    {
        private static readonly Color DEFAULT_BORDER_COLOR = Color.FromRgb(171, 173, 179);
        public static readonly Brush DEFAULT_BORDER_BRUSH = new SolidColorBrush(DEFAULT_BORDER_COLOR);
        public static readonly Brush INVALID_BORDER_BRUSH = new SolidColorBrush(Colors.Red);
    }
}

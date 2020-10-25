namespace Sanchez.DansUI.Models
{
    public class BoundingRect
    {
        public double x { get; set; }
        public double y { get; set; }

        public double width { get; set; }
        public double height { get; set; }

        public double top { get; set; }
        public double right { get; set; }
        public double bottom { get; set; }
        public double left { get; set; }

        public double MidX => left + (width / 2);
        public double MidY => top + (height / 2);
    }
}

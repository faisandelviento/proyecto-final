// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BackgroundColor
    {
        public int b { get; set; }
        public string closest_palette_color { get; set; }
        public string closest_palette_color_html_code { get; set; }
        public string closest_palette_color_parent { get; set; }
        public double closest_palette_distance { get; set; }
        public int g { get; set; }
        public string html_code { get; set; }
        public double percent { get; set; }
        public int r { get; set; }
    }

    public class Colors
    {
        public List<BackgroundColor> background_colors { get; set; }
        public double color_percent_threshold { get; set; }
        public int color_variance { get; set; }
        public List<ForegroundColor> foreground_colors { get; set; }
        public List<ImageColor> image_colors { get; set; }
        public double object_percentage { get; set; }
    }

    public class ForegroundColor
    {
        public int b { get; set; }
        public string closest_palette_color { get; set; }
        public string closest_palette_color_html_code { get; set; }
        public string closest_palette_color_parent { get; set; }
        public double closest_palette_distance { get; set; }
        public int g { get; set; }
        public string html_code { get; set; }
        public double percent { get; set; }
        public int r { get; set; }
    }

    public class ImageColor
    {
        public int b { get; set; }
        public string closest_palette_color { get; set; }
        public string closest_palette_color_html_code { get; set; }
        public string closest_palette_color_parent { get; set; }
        public double closest_palette_distance { get; set; }
        public int g { get; set; }
        public string html_code { get; set; }
        public double percent { get; set; }
        public int r { get; set; }
    }

    public class Result
    {
        public Colors colors { get; set; }
    }

    public class Root
    {
        public Result result { get; set; }
        public Status status { get; set; }
    }

    public class Status
    {
        public string text { get; set; }
        public string type { get; set; }
    }


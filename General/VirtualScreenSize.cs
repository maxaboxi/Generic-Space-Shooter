namespace spacerpg.General
{
    static class VirtualScreenSize
    {
        public static int Width { get; }
        public static int Height { get; }
        public static int ScreenSizeMultiplier { get; set; }

        static VirtualScreenSize()
        {
            Width = 480;
            Height = 270;
        }
    }
}

using System.Drawing;


namespace ConsoleBattleship.Screen
{
  internal class Colors
  {
    public static readonly Color Background             = Color.FromArgb(0, 10, 10);      // Dark Cyan
    public static readonly Color HighlightedBackground  = Color.FromArgb(100, 100, 100);  // Grey
    public static readonly Color BackgroundChar         = Color.FromArgb(0, 100, 100);    // Cyan
    public static readonly Color Crosshair              = Color.FromArgb(250, 250, 120);  // Yellow
    public static readonly Color Border                 = Color.FromArgb(165, 229, 250);  // Blue
    public static readonly Color HighlightedBorder      = Color.FromArgb(0, 200, 200);    // Cyan
    public static readonly Color InputPrompt            = Color.FromArgb(250, 129, 120);  // Red
    public static readonly Color Battleship             = Color.FromArgb(155, 55, 20);    // Brown
  }
}

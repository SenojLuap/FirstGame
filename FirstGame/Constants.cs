
namespace paujo.FirstGame {
  public class Constants {
    
    public class Paths {
      public static string TileDirectory = @"Content\tilesheets";
    }

    public class Layers {
      public static int Background = 10;

      public static int Environment = 20;
      
      public static int EntityBack = 110;
      public static int EntityMid = 120;
      public static int EntityFwd = 130;

    }

    public class Application {
      public static int RenderWidth = 256;
      public static int RenderHeight = 224;
    }

    public class World {
      public static int GridCellWidth = 32;
      public static int GridCellHeight = 32;
    }

  }

  public enum Direction : byte { North, South, East, West, NorthEast, NorthWest, SouthEast, SouthWest };
}

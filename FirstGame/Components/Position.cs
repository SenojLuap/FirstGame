using Microsoft.Xna.Framework;

using Newtonsoft.Json;

namespace paujo.FirstGame.Components {
  public class Position : IComponent {
    
    public static long PositionComponentType = 0x2;

    [JsonIgnore]
    public long ComponentType {
      get { return PositionComponentType; }
    }

    public int X {
      get; set;
    }

    public int Y {
      get; set;
    }

    public Point Point {
      get { return new Point(X, Y); }
    }
  }
}

using Newtonsoft.Json;

using paujo.FirstGame;


namespace paujo.FirstGame.Components {
  public class Facing : IComponent {
    
    public static long FacingComponentType = 0x4;

    [JsonIgnore]
    public long ComponentType {
      get { return FacingComponentType; }
    }

    public Direction Direction {
      get; set;
    }

    public Facing(Direction direction) {
      Direction = direction;
    }
  }
}

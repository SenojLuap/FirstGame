
namespace paujo.FirstGame.Components {
  class Scale : IComponent {

    public static long ScaleComponentType = 0x20;

    public long ComponentType {
      get; set;
    }

    public float Factor {
      get; set;
    }

    
    public Scale(float? scale = 1f) {
      Factor = (float)scale;
    }
  }
}

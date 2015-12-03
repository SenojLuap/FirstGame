using Microsoft.Xna.Framework;

namespace paujo.FirstGame {
  abstract public class MovingEntity : WorldEntity {

    public Vector2 Motion {
      get; set;
    }

    // Speed in pixels per second
    virtual public int Speed {
      get; set;
    } = 1;

    
    virtual public void Move(double deltaTime) {
      deltaTime /= 1000.0;
      RealPos += Vector2.Multiply(Motion, (float)Speed * (float)deltaTime);
    }
  }
}

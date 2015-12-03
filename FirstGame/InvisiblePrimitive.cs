using Microsoft.Xna.Framework.Graphics;

namespace paujo.FirstGame {
  public class InvisiblePrimitive : IGraphicsPrimitive {
    private InvisiblePrimitive() { }

    public void Draw(FirstGame game, SpriteBatch spriteBatch) {
    }

    private static InvisiblePrimitive _instance;

    static InvisiblePrimitive() {
      _instance = new InvisiblePrimitive();
    }

    public static InvisiblePrimitive Instance {
      get { return _instance; }
    }
  }
}

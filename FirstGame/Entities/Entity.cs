using Microsoft.Xna.Framework;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public class Entity {

    public Game Game {
      get; set;
    }

    public Entity(Game game) {
      Game = game;
    }
    public virtual void Update(GameTime gameTime) { }

    public virtual void Draw(GameTime gameTime, Renderer renderer) { }
  }
}

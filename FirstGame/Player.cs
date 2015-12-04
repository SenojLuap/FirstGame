using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public class Player : MovingEntity {

    public Player(FirstGame game) : base() {
      Speed = 50;
      DrawHelper = new SpriteHelper(game.TileSheets["girlFarmer"], 1, Pos,
				    depth: (float)Pos.Y / (float)Constants.Application.RenderHeight);
    }

    /**
     * World Entity
     */
    override public void GameTick(FirstGame game, double deltaTime) {
      UpdateMotion(game);
      Move(deltaTime);
      SpriteHelper helper = DrawHelper as SpriteHelper;
      if (helper != null) {
	helper.Pos = Pos;
	helper.Depth = (float)Pos.Y / (float)game.Resolution.Y;
      }
    }
    

    public void UpdateMotion(FirstGame game) {
      KeyboardState state = Keyboard.GetState();
      Vector2 newMotion = new Vector2(0f, 0f);
      if (state.IsKeyDown(Keys.Up))
	newMotion.Y = -1f;
      else if (state.IsKeyDown(Keys.Down))
	newMotion.Y = 1f;
      if (state.IsKeyDown(Keys.Left))
	newMotion.X = -1f;
      else if (state.IsKeyDown(Keys.Right))
	newMotion.X = 1f;
      Motion = newMotion;
    }

    
  }
}

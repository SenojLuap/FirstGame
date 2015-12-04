using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public class Player : MovingEntity {

    
    public Direction Facing {
      get; set;
    }


    public Player(FirstGame game) : base() {
      Speed = 60;
      DrawHelper = new SpriteHelper(game.TileSheets["girlFarmer"], 1, Pos,
				    depth: (float)Pos.Y / (float)Constants.Application.RenderHeight);
      Facing = Direction.South;
    }


    /**
     * World Entity
     */
    override public void GameTick(FirstGame game, double deltaTime) {
      UpdateMotion(game);
      Move(deltaTime);

      Direction oldFacing = Facing;
      UpdateFacing();
      if (Facing != oldFacing) UpdateGraphics(game);

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


    public void UpdateFacing() {
      if (!(Motion.LengthSquared() > 0f)) return;
      if (Math.Abs(Motion.X) > Math.Abs(Motion.Y)) {
	Facing = (Motion.X > 0f) ? Direction.East : Direction.West;
      } else {
	Facing = (Motion.Y > 0f) ? Direction.South : Direction.North;
      }
    }


    public void UpdateGraphics(FirstGame game) {
      if (Facing == Direction.South)
	DrawHelper = new SpriteHelper(game.TileSheets["girlFarmer"], 1);
      else if (Facing == Direction.West)
	DrawHelper = new SpriteHelper(game.TileSheets["girlFarmer"], 4);
      else if (Facing == Direction.East)
	DrawHelper = new SpriteHelper(game.TileSheets["girlFarmer"], 7);
      else if (Facing == Direction.North)
	DrawHelper = new SpriteHelper(game.TileSheets["girlFarmer"], 10);
    }
  }
}

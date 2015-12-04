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
      bool wasMoving = IsMoving();
      UpdateMotion(game);
      Move(game, deltaTime);
      
      Direction oldFacing = Facing;
      UpdateFacing();
      if ((Facing != oldFacing) || (wasMoving != IsMoving())) UpdateGraphics(game);

      TileSheetHelper helper = DrawHelper as TileSheetHelper;
      if (helper != null) {
	helper.Update(deltaTime);
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
      TileSheet tileSheet = game.TileSheets["girlFarmer"];
      Misc.pln("Update Graphics");
      if (Facing == Direction.South)
	DrawHelper = IsMoving() ? tileSheet.GetAnimationHelper("girlDown") : tileSheet.GetSpriteHelper(1);
      else if (Facing == Direction.West)
	DrawHelper = IsMoving() ? tileSheet.GetAnimationHelper("girlLeft") : tileSheet.GetSpriteHelper(4);
      else if (Facing == Direction.East)
	DrawHelper = IsMoving() ? tileSheet.GetAnimationHelper("girlRight") : tileSheet.GetSpriteHelper(7);
      else if (Facing == Direction.North)
	DrawHelper = IsMoving() ? tileSheet.GetAnimationHelper("girlUp") : tileSheet.GetSpriteHelper(10);
    }
  }
}

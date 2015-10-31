using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public class PlayerEntity : SpriteEntity {

    public float PlayerSpeed {
      get; set;
    } = 150.0f;

    public AnimationHelper AnimHelper {
      get; set;
    }

    public PlayerEntity(Game game) : base(game) {
      Pos = new Vector2(20f, 20f);
      Frame = 1;
      FirstGame fGame = game as FirstGame;
      if (fGame != null) {
	TileSheet = fGame.TileSheets["girlFarmer"];
	Animation anim = TileSheet.AnimationByKey("girlDown");
	AnimHelper = anim.GetHelper();
      }
    }


    public override void Update(GameTime gameTime) {
      GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
      if (capabilities.IsConnected) {
	GamePadState state = GamePad.GetState(PlayerIndex.One);
	if (capabilities.HasLeftXThumbStick) {
	  Vector2 movement = Vector2.Normalize(state.ThumbSticks.Left) * (float)gameTime.ElapsedGameTime.TotalSeconds * PlayerSpeed;
	  movement.Y *= -1f;
	  if (movement.Length() > 0f)
	    Pos += movement;
	}
      }
    }


    public override void Draw(GameTime gameTime, Renderer renderer) {
      AnimHelper.Update(gameTime);
      Frame = AnimHelper.Frame();
      base.Draw(gameTime, renderer);
    }
  }
}

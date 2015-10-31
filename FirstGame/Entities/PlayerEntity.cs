using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public class PlayerEntity : MovingEntity {

    public float PlayerSpeed {
      get; set;
    } = 150.0f;


    public PlayerEntity(Game game) : base(game, null) {
      Pos = new Vector2(20f, 20f);
      FirstGame fGame = game as FirstGame;
      if (fGame != null) {
	TileSheet = fGame.TileSheets["girlFarmer"];
      }
    }


    public override void Initialize() {
      SetStationaryFrames(10, 7, 1, 4);
      SetMovingAnimations("girlUp", "girlRight", "girlDown", "girlLeft");
      base.Initialize();
    }


    public override void Update(GameTime gameTime) {
      GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
      if (capabilities.IsConnected) {
	GamePadState state = GamePad.GetState(PlayerIndex.One);
	if (capabilities.HasLeftXThumbStick) {
	  Vector2 movement = Vector2.Normalize(state.ThumbSticks.Left) * (float)gameTime.ElapsedGameTime.TotalSeconds * PlayerSpeed;
	  movement.Y *= -1f;
	  Move(movement);
	}
      }
    }

  }
}

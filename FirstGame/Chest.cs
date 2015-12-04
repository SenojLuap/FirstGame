using paujo.GameUtility;

namespace paujo.FirstGame {
  public class Chest : WorldEntity {

    public Chest(FirstGame game) : base() {
      DrawHelper = new SpriteHelper(game.TileSheets["chests"], 0, pos: Pos,
				    depth: (float)Pos.Y / (float)Constants.Application.RenderHeight);
    }

    
    override public void GameTick(FirstGame game, double deltaTime) {
      // Nothing
    }


    override public void Activate(Player player) {
      Misc.pln("Chest Activated");
    }
  }
}

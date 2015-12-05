using paujo.GameUtility;

namespace paujo.FirstGame {
  public class Chest : WorldEntity {

    private IRenderJob _cachedJob;

    public Chest(FirstGame game) : base() {
      DrawHelper = new SpriteHelper(game.TileSheets["chests"], 0, pos: Pos,
				    depth: (float)Pos.Y / (float)Constants.Application.RenderHeight);
      _cachedJob = null;
    }

    
    override public void GameTick(FirstGame game, double deltaTime) {
      // Nothing
    }


    override public void Activate(Player player) {
      Misc.pln("Chest Activated");
    }


    override public IRenderJob GetRenderJob(FirstGame game) {
      if (_cachedJob == null)
	_cachedJob = base.GetRenderJob(game);
      return _cachedJob;
    }


    override public void InvalidateGraphics(FirstGame game) {
      _cachedJob = null;
      SpriteHelper helper = DrawHelper as SpriteHelper;
      if (helper != null) {
	helper.Pos = Pos;
	helper.Depth = (float)Pos.Y / (float)Constants.Application.RenderHeight;
      }
    }
  }
}

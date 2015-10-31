using paujo.GameUtility;

using Microsoft.Xna.Framework;

namespace paujo.FirstGame {
  public class SpriteEntity : Entity {

    
    public Vector2 Pos {
      get; set;
    } = new Vector2(0.0f, 0.0f);


    public TileSheet TileSheet {
      get; set;
    }

    public int Layer {
      get; set; 
    } = Constants.Layers.EntityMid;


    public TileSheetHelper DrawHelper {
      get; set;
    }

    public int Frame {
      get; set;
    }


    public SpriteEntity(Game game, TileSheet tileSheet, int frame) : base(game) {
      TileSheet = tileSheet;
      Frame = frame;
    }


    public virtual void Initialize() {
      ResetHelper();
    }


    public override void Draw(GameTime gameTime, Renderer renderer) {
      if (DrawHelper != null) {
	DrawHelper.Pos = Pos;
	DrawHelper.Update(gameTime);
	renderer.AddJob(DrawHelper.GetRenderJob(), Layer);
      }
    }


    public virtual void ResetHelper() {
      DrawHelper = null;
      if (TileSheet != null && Frame >= 0 && Frame < TileSheet.TileCount)
	DrawHelper = (TileSheetHelper)TileSheet.GetSpriteHelper(Frame);
    }
  }
}

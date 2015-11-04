using paujo.GameUtility;

using Microsoft.Xna.Framework;

namespace paujo.FirstGame {
  public class SpriteEntity : Entity {

    
    public Vector2 Pos {
      get; set;
    } = new Vector2(0f, 0f);


    public TileSheet TileSheet {
      get; set;
    }

    public int Layer {
      get; set; 
    } = Constants.Layers.EntityMid;


    public TileSheetHelper DrawHelper {
      get; set;
    }

    public virtual int Frame {
      get; set;
    }

    public int DepthShift {
      get; set;
    } = 0;


    public SpriteEntity(FirstGame game, TileSheet tileSheet, int frame) : base(game) {
      TileSheet = tileSheet;
      Frame = frame;
    }


    public virtual void Initialize() {
      ResetHelper();
    }


    public override void Draw(GameTime gameTime, Renderer renderer) {
      if (DrawHelper != null) {
	DrawHelper.Pos = Misc.Vector2ToPoint(Pos) - new Point(TileSheet.FrameWidth / 2, TileSheet.FrameHeight);
	DrawHelper.Depth = (Pos.Y - DepthShift) / (float)Game.Resolution.Y;
	Misc.pln("depth: " + DrawHelper.Depth);
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

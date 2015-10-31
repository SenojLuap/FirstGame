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


    public int Frame {
      get; set;
    }


    public SpriteEntity(Game game) : base(game) { }


    public override void Draw(GameTime gameTime, Renderer renderer) {
      TileSheetRenderJob job = new TileSheetRenderJob();
      job.Frame = Frame;
      job.Pos = Pos;
      job.TileSheet = TileSheet;
      renderer.AddJob(job, Layer);
    }
  }
}

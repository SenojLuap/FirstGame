using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public class SpritePrimitive : IGraphicsPrimitive {

    public Point Pos {
      get; set;
    }

    public string TileSheetKey {
      get; set;
    }

    public int Frame {
      get; set;
    }

    
    public SpritePrimitive(Point pos, string tileSheetkey, int frame) {
      Pos = pos;
      TileSheetKey = tileSheetkey;
      Frame = frame;
    }


    public void Draw(FirstGame game, SpriteBatch spriteBatch) { 
      Misc.pln("TODO");
    }
  }
}

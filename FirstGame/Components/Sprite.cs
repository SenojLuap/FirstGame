using paujo.GameUtility;

namespace paujo.FirstGame.Components {
  public class Sprite : IComponent {

    public static long SpriteComponentType = 0x10;
    
    public long ComponentType {
      get { return SpriteComponentType; }
    }

    public string TileSheet {
      get; set;
    }

    public int Frame {
      get; set;
    }

    
    public Sprite(TileSheet tileSheet, int frame) {
      TileSheet = tileSheet.TextureKey;
      Frame = frame;
    }


    public Sprite(string tileSheet, int frame) {
      TileSheet = tileSheet;
      Frame = frame;
    }

  }
}

using paujo.FirstGame;
using paujo.GameUtility;

using Microsoft.Xna.Framework;

namespace paujo.FirstGame.Components {
  public class AnimationState : AnimationHelper, IComponent {

    public static long AnimationStateComponentType = 0x8;

    public long ComponentType {
      get { return AnimationStateComponentType; }
    }

    
    public AnimationState(TileSheet tileSheet, Animation animation,
			  Point? pos = null, float scale = 1f, float depth = 1f) :
    base(tileSheet, animation, pos, scale, depth) {
    }
  }
}

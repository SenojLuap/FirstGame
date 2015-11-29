using paujo.GameUtility;

using Microsoft.Xna.Framework;

namespace paujo.FirstGame {
  public interface ICollisionBound {
    bool CollidesWith(ICollisionBound otherBound);
  }

  
  public class CircleBound : ICollisionBound {

    public Circle Circle {
      get; set;
    }

    
    public CircleBound(Circle circle) {
      this.Circle = circle;
    }
    
    public bool CollidesWith(ICollisionBound otherBound) {
      CircleBound otherCircle = otherBound as CircleBound;
      if (otherCircle != null) {
	return Collision.Collides(Circle, otherCircle.Circle);
      }
      RectBound otherRect = otherBound as RectBound;
      if (otherRect != null) {
	return Collision.Collides(otherRect.Rect, Circle);
      }
      return false;
    }
  }


  public class RectBound : ICollisionBound {
    
    public Rectangle Rect {
      get; set;
    }


    public RectBound(Rectangle rect) {
      this.Rect = rect;
    }


    public bool CollidesWith(ICollisionBound otherBound) {
      RectBound otherRect = otherBound as RectBound;
      if (otherRect != null) {
	return Collision.Collides(Rect, otherRect.Rect);
      }
      CircleBound otherCircle = otherBound as CircleBound;
      if (otherCircle != null) {
	return Collision.Collides(Rect, otherCircle.Circle);
      }
      return false;
    }
  }
}

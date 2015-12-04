using System.Collections.Generic;

using Microsoft.Xna.Framework;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public abstract class WorldEntity {

    public IDrawHelper DrawHelper {
      get; set;
    }

    public Point Pos {
      get {
	return RealPos.ToPoint();
      }
      set {
	RealPos = Misc.PointToVector2(value);
      }
    }

    public Vector2 RealPos {
      get; set;
    }
    

    abstract public void GameTick(FirstGame game, double deltaTime);


    virtual public IRenderJob GetRenderJob(FirstGame game) {
      if (DrawHelper != null) return DrawHelper.GetRenderJob();
      return InvisibleRenderJob.Instance();
    }

    public ICollisionBound GetFastBound() {
      return new CircleBound(new Circle(Pos, 1));;
    }

    
    public List<ICollisionBound> GetAccurateBound() {
      List<ICollisionBound> res = new List<ICollisionBound>();
      res.Add(GetFastBound());
      return res;
    }
  }
}

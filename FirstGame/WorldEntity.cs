using System.Collections.Generic;

using Microsoft.Xna.Framework;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public abstract class WorldEntity {

    public IDrawHelper DrawHelper {
      get; set;
    }

    
    public Point GridPos {
      get {
	int x = Pos.X / Constants.World.GridCellWidth;
	int y = Pos.Y / Constants.World.GridCellHeight;
	return new Point(x, y);
      }
      set {
	int x = value.X * Constants.World.GridCellWidth;
	x += Constants.World.GridCellWidth / 2;
	int y = value.Y * Constants.World.GridCellHeight;
	y += Constants.World.GridCellHeight / 2;
	Pos = new Point(x, y);
      }
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


    virtual public void Activate(Player player) {
      // Do Nothing
    }
  }
}

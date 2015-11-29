using System.Collections.Generic;

using Microsoft.Xna.Framework;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public abstract class WorldEntity {

    public Point Pos {
      get; set;
    }
    

    abstract public void GameTick(double deltaTime);


    abstract public void Draw(FirstGame game, double deltaTime);


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

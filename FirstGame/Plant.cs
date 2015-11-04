using Microsoft.Xna.Framework;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public class Plant : SpriteEntity {
    
    public double GrowthTime {
      get; set;
    } = 0f;

    public override int Frame {
      get {
	if (GrowthTime < 5.0)
	  return 0;
	if (GrowthTime < 10.0)
	  return 9;
	if (GrowthTime < 15.0)
	  return 18;
	return 27;
      }
    }


    public Plant(FirstGame game) : base(game, game.TileSheets["plants"], -1) {
      Pos = new Vector2(100f, 100f);
      DepthShift = 3;
    }


    public override void Update(GameTime gameTime) {
      int prevFrame = Frame;
      GrowthTime += gameTime.ElapsedGameTime.TotalSeconds;
      if (Frame != prevFrame)
	ResetHelper();
      base.Update(gameTime);
    }


  }
}

using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

namespace paujo.FirstGame {
  class PrimitiveList : IGraphicsPrimitive {

    public List<IGraphicsPrimitive> List {
      get; set;
    }

    public PrimitiveList() : base() {
      List = new List<IGraphicsPrimitive>();
    }

    
    public void Draw(FirstGame game, SpriteBatch spriteBatch) {
      foreach(var prim in List) {
	prim.Draw(game, spriteBatch);
      }
    }

    
    public void Add(IGraphicsPrimitive prim) {
      List.Add(prim);
    }
  }
}

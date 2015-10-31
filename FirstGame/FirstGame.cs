using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public class FirstGame : Game {
    GraphicsDeviceManager graphics;

    public Dictionary<string, TileSheet> TileSheets {
      get; set;
    }

    public ContentManager TileContent {
      get; set;
    }

    public List<Entity> Entities {
      get; set;
    }

    public Renderer Renderer {
      get; set;
    }
    
    public FirstGame() {
      graphics = new GraphicsDeviceManager(this);
      TileSheets = new Dictionary<string, TileSheet>();
      Content.RootDirectory = "Content";

      TileContent = new ContentManager(Services);
      TileContent.RootDirectory = Constants.Paths.TileDirectory;

      Entities = new List<Entity>();
      Renderer = new Renderer(this);
    }
    
    
    protected override void Initialize() {
      base.Initialize();
      PlayerEntity player = new PlayerEntity(this);
      player.Initialize();
      Entities.Add(player);
    }

    
    protected override void LoadContent() {
      if (!LoadTileSheets()) Exit();
      foreach (KeyValuePair<string, TileSheet> keyValue in TileSheets) {
	System.Console.WriteLine("Loaded TileSheet: " + keyValue.Key);
      }
    }

    
    public bool LoadTileSheets() {
      string[] files = null;
      try {
	files = Directory.GetFiles(Constants.Paths.TileDirectory);
      } catch (Exception e) {
	System.Diagnostics.Debug.WriteLine("Exception while reading directory: " + e.Message);
	return false;
      }
      if (files != null) {
	foreach (string file in files) {
	  if (file.EndsWith(".jts", StringComparison.OrdinalIgnoreCase)) {
	    TileSheet newTileSheet = TileSheet.ReadFromFile(file);
	    if (newTileSheet != null) {
	      try {
		TileContent.Load<Texture2D>(newTileSheet.TextureKey);
	      } catch (ContentLoadException e) {
		System.Diagnostics.Debug.WriteLine("Error while reading file" + newTileSheet.TextureKey + ": " + e.Message);
		Exit();
	      }
	      TileSheets.Add(newTileSheet.TextureKey, newTileSheet);
	      newTileSheet.GetTexture = GetTileSheet;
	    }
	  }
	}
      }
      return true;
    }


    public Texture2D GetTileSheet(string filename) {
      return TileContent.Load<Texture2D>(filename);
    }

    
    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent() {
      // TODO: Unload any non ContentManager content here
    }
    
    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
	Exit();
      foreach (var entity in Entities)
	entity.Update(gameTime);
      base.Update(gameTime);
    }
    
    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      Renderer.Reset();
      foreach (var entity in Entities) {
	entity.Draw(gameTime, Renderer);
      }

      Renderer.Draw();
      base.Draw(gameTime);
    }
  }
}

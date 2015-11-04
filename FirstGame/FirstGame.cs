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
    RenderTarget2D renderTarget;

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

    public Point Resolution {
      get {
	return new Point(Convert.ToInt32(Constants.Application.RenderWidth * RenderScale),
			 Convert.ToInt32(Constants.Application.RenderHeight * RenderScale));
      }
    }

    public float RenderScale {
      get; set;
    } = 2f;
    
    public FirstGame() {
      graphics = new GraphicsDeviceManager(this);
      graphics.PreferredBackBufferWidth = Constants.Application.RenderWidth * 2;
      graphics.PreferredBackBufferHeight = Constants.Application.RenderHeight * 2;
      graphics.ApplyChanges();
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

      Plant plant = new Plant(this);
      plant.Initialize();
      Entities.Add(plant);
      
      Primitives.Initialize(this);
      renderTarget = new RenderTarget2D(GraphicsDevice, Constants.Application.RenderWidth, Constants.Application.RenderHeight,
					false, GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
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

    
    protected override void UnloadContent() {
      // TODO: Unload any non ContentManager content here
    }
    

    protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
	Exit();
      foreach (var entity in Entities)
	entity.Update(gameTime);
      base.Update(gameTime);
    }

    
    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.SetRenderTarget(renderTarget);
      GraphicsDevice.Clear(Color.CornflowerBlue);

      Renderer.Reset();
      foreach (var entity in Entities) {
	entity.Draw(gameTime, Renderer);
      }
      Renderer.Draw();
      
      GraphicsDevice.SetRenderTarget(null);

      SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
      spriteBatch.Begin(samplerState: SamplerState.PointClamp);
      spriteBatch.Draw(renderTarget, new Rectangle(0, 0, Constants.Application.RenderWidth * 2,
						   Constants.Application.RenderHeight * 2), Color.White);
      spriteBatch.End();

      
      base.Draw(gameTime);
    }
  }
}

﻿using System;
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

    public Renderer Renderer {
      get; set;
    }

    public Dictionary<int, Dictionary<int, List<WorldEntity>>> EntityPos {
      get; set;
    }

    public List<WorldEntity> Entities {
      get; set;
    }

    public AdvancedKeyboard AdvKeyboard {
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

      Renderer = new Renderer(this);
    }
    
    
    protected override void Initialize() {
      base.Initialize();
      
      Primitives.Initialize(this);
      renderTarget = new RenderTarget2D(GraphicsDevice,
					Constants.Application.RenderWidth,
					Constants.Application.RenderHeight,
					false,
					GraphicsDevice.PresentationParameters.BackBufferFormat,
					DepthFormat.Depth24);

      EntityPos = new Dictionary<int, Dictionary<int, List<WorldEntity>>>();
      Entities = new List<WorldEntity>();

      Player player = new Player(this);
      player.Pos = new Point(Constants.Application.RenderWidth / 2,
			     Constants.Application.RenderHeight / 2);
      Entities.Add(player);
      PutDown(player);

      Chest chest = new Chest(this);
      chest.GridPos = new Point(3, 3);
      chest.InvalidateGraphics(this);
      Entities.Add(chest);
      PutDown(chest);
      

      AdvKeyboard = new AdvancedKeyboard();
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


    /**
     * Manage Entity List
     */
    public bool PickUp(WorldEntity entity) {
      Point entPos = entity.GridPos;
      if (EntityPos.ContainsKey(entPos.X)) {
	Dictionary<int, List<WorldEntity>> yMap = EntityPos[entPos.X];
	if (yMap.ContainsKey(entPos.Y)) {
	  List<WorldEntity> entList = yMap[entPos.Y];
	  if (entList.Contains(entity)) {
	    entList.Remove(entity);
	    return true;
	  }
	}
      }
      return false;
    }


    public void PutDown(WorldEntity entity) {
      Dictionary<int, List<WorldEntity>> yMap;
      if (!EntityPos.ContainsKey(entity.GridPos.X)) {
	yMap = new Dictionary<int, List<WorldEntity>>();
	EntityPos.Add(entity.GridPos.X, yMap);
      } else {
	yMap = EntityPos[entity.GridPos.X];
      }
      List<WorldEntity> list;
      if (!yMap.ContainsKey(entity.GridPos.Y)) {
	list = new List<WorldEntity>();
	yMap.Add(entity.GridPos.Y, list);
      } else {
	list = yMap[entity.GridPos.Y];
      }
      list.Add(entity);
    }

    
    public List<WorldEntity> GetEntitiesAtGridPos(Point gridPos) {
      if (EntityPos.ContainsKey(gridPos.X)) {
	Dictionary<int, List<WorldEntity>> yMap = EntityPos[gridPos.X];
	if (yMap.ContainsKey(gridPos.Y)) {
	  List<WorldEntity> list = yMap[gridPos.Y];
	  return list;
	}
      }
      return new List<WorldEntity>();
    }

    
    public WorldEntity GetEntityAtGridPos(Point gridPos) {
      List<WorldEntity> list = GetEntitiesAtGridPos(gridPos);
      if (list.Count > 0) return list[0];
      return null;
    }
    

    protected override void Update(GameTime gameTime) {
      AdvKeyboard.Update(Keyboard.GetState());

      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
	  AdvKeyboard.IsKeyDown(Keys.Escape))
	Exit();
      
      foreach (var entity in Entities)
	entity.GameTick(this, gameTime.ElapsedGameTime.TotalMilliseconds);

      base.Update(gameTime);
    }

    
    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.SetRenderTarget(renderTarget);
      GraphicsDevice.Clear(Color.CornflowerBlue);

      Renderer.Reset();

      foreach (var entity in Entities)
	Renderer.AddJob(entity.GetRenderJob(this), 1);
      
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

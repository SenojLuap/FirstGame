using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using paujo.GameUtility;

namespace paujo.FirstGame {
  public class MovingEntity : SpriteEntity {
    
    public Direction Facing {
      get; set;
    }

    public Dictionary<Direction, int> StationaryFrames {
      get; set;
    }

    public Dictionary<Direction, string> MovingAnimations {
      get; set;
    }

    public bool Moving {
      get; set;
    }

    public MovingEntity(Game game, TileSheet tileSheet = null, Direction? facing = null) : base(game, tileSheet, -1) {
      StationaryFrames = new Dictionary<Direction, int>();
      MovingAnimations = new Dictionary<Direction, string>();
      if (facing == null) facing = Direction.South;
      Facing = (Direction)facing;
    }


    public void SetStationaryFrame(Direction dir, int frame) {
      StationaryFrames.Add(dir, frame);
    }

    
    public void SetStationaryFrames(int northFrame, int eastFrame, int southFrame, int westFrame) {
      SetStationaryFrame(Direction.North, northFrame);
      SetStationaryFrame(Direction.East, eastFrame);
      SetStationaryFrame(Direction.South, southFrame);
      SetStationaryFrame(Direction.West, westFrame);
    }


    public void SetMovingAnimation(Direction dir, string animationKey) {
      MovingAnimations.Add(dir, animationKey);
    }

    
    public void SetMovingAnimations(string northAnimKey, string eastAnimKey, string southAnimKey, string westAnimKey) {
      SetMovingAnimation(Direction.North, northAnimKey);
      SetMovingAnimation(Direction.East, eastAnimKey);
      SetMovingAnimation(Direction.South, southAnimKey);
      SetMovingAnimation(Direction.West, westAnimKey);
    }


    public virtual void Move(Vector2 movement) {
      bool newMoving = true;
      Direction newFacing = Facing;
      
      if (movement.Length() > 0) {
	Pos += movement;
      } else {
	newMoving = false;
      }
      
      if (newMoving) {
	if (Math.Abs(movement.X) > Math.Abs(movement.Y)) {
	  newFacing = (movement.X > 0 ? Direction.East : Direction.West);
	} else {
	  newFacing = (movement.Y > 0 ? Direction.South : Direction.North);
	}
      }
      
      if (newMoving != Moving || newFacing != Facing) {
	Moving = newMoving;
	Facing = newFacing;
	ResetHelper();
      }
    }


    public override void ResetHelper() {
      DrawHelper = null;
      if (Moving) {
	if (MovingAnimations.ContainsKey(Facing)) {
	  string anim = MovingAnimations[Facing];
	  DrawHelper = (TileSheetHelper)TileSheet.GetAnimationHelper(anim);
	}
      } else {
	if (StationaryFrames.ContainsKey(Facing)) {
	  int frame = StationaryFrames[Facing];
	  DrawHelper = (TileSheetHelper)TileSheet.GetSpriteHelper(frame);
	}
      }
    }
  }
}
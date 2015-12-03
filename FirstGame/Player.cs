namespace paujo.FirstGame {
  public class Player : MovingEntity {

    /**
     * World Entity
     */
    override public void GameTick(FirstGame game, double deltaTime) {
      Move(deltaTime);
    }
    
  }
}

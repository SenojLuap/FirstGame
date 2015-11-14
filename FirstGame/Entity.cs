using System.Collections.Generic;

namespace paujo.FirstGame {
  public class Entity : IComponent {

    public static long EntityComponentType = 1;
    
    public virtual long ComponentType {
      get {
	return EntityComponentType;
      }
    }

    public long _ComponentMask;
    public virtual long ComponentMask {
      get {
	if (_ComponentMask < 0) {
	  _ComponentMask = 0;
	  foreach (var cmp in Components)
	    _ComponentMask |= cmp.ComponentType;
	}
	return _ComponentMask;
      }
    }
    
    public List<IComponent> Components {
      get; private set;
    }


    public Entity() {
      Components = new List<IComponent>();
    }
    

    public virtual void AddComponent(IComponent newComponent) {
      _ComponentMask = -1;
      Components.Add(newComponent);
    }


    public virtual void RemoveComponent(IComponent oldComponent) {
      if (Components.Contains(oldComponent)) {
	_ComponentMask = -1;
	Components.Remove(oldComponent);
      }
    }
    
  }
}

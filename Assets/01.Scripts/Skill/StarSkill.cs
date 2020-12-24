using UnityEngine;

public class StarSkill : ISkill {
    public GestureType GestureType => GestureType.Star;
    
    public void Activate() {
        Debug.Log("Star skill activated");
    }
}
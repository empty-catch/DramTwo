using UnityEngine;

public class HeartSkill : ISkill {
    public GestureType GestureType => GestureType.Heart;
    
    public void Activate() {
        Debug.Log("Heart skill activated");
    }
}
using UnityEngine;

public class ClockSkill : ISkill {
    public GestureType GestureType => GestureType.Clock;
    
    public void Activate() {
        Debug.Log("Clock skill activated");
    }
}
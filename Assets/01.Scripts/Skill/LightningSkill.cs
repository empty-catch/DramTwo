using UnityEngine;

public class LightningSkill : ISkill {
    public GestureType GestureType => GestureType.Lightning;
    
    public void Activate() {
        Debug.Log("Lightning skill activated");
    }
}
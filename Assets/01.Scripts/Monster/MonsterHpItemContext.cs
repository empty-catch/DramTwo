using Slash.Unity.DataBind.Core.Data;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpItemContext : Context {
    private Property<Sprite> hpGestureProperty = new Property<Sprite>();
    public Sprite HpGesture {
        get => hpGestureProperty.Value;
        set => hpGestureProperty.Value = value;
    }
    
    public void SettingGesture(GestureType key) {
        HpGesture = MonsterGestureResources.Instance.GestureItems[key];
    }

    public void ResetItem() {
        HpGesture = null;
    }
}
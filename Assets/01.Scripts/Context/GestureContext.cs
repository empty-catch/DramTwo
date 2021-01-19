using Slash.Unity.DataBind.Core.Data;
using UnityEngine;

public class GestureContext : Context {
    private readonly Property<bool> activeProperty = new Property<bool>();
    private readonly Property<Sprite> spriteProperty = new Property<Sprite>();

    public bool Active {
        get => activeProperty.Value;
        set => activeProperty.Value = value;
    }

    public Sprite Sprite {
        get => spriteProperty.Value;
        set => spriteProperty.Value = value;
    }
}
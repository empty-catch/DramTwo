using System.Collections;
using Slash.Unity.DataBind.Core.Data;
using Tempus.CoroutineTools;
using UnityEngine;

public class PlayerCharacterContext : Context {
    private readonly Property<float> timerFillProperty = new Property<float>();
    private readonly Property<GestureContext> gesture0Property = new Property<GestureContext>(new GestureContext());
    private readonly Property<GestureContext> gesture1Property = new Property<GestureContext>(new GestureContext());
    private readonly Property<GestureContext> gesture2Property = new Property<GestureContext>(new GestureContext());
    private readonly Property<GestureContext> gesture3Property = new Property<GestureContext>(new GestureContext());
    private readonly Property<GestureContext> gesture4Property = new Property<GestureContext>(new GestureContext());

    public PlayerCharacterContext() {
        PlayerCharacterController.Instance.TimerFilled += fillAmount => TimerFill = fillAmount;
        PlayerCharacterController.Instance.GestureActiveSet += (index, active) => Gesture(index).Active = active;
        PlayerCharacterController.Instance.GestureSpriteSet += (index, sprite) => Gesture(index).Sprite = sprite;
    }

    public float TimerFill {
        get => timerFillProperty.Value;
        private set => timerFillProperty.Value = value;
    }

    public GestureContext Gesture0 {
        get => gesture0Property.Value;
        private set => gesture0Property.Value = value;
    }

    public GestureContext Gesture1 {
        get => gesture1Property.Value;
        private set => gesture1Property.Value = value;
    }

    public GestureContext Gesture2 {
        get => gesture2Property.Value;
        private set => gesture2Property.Value = value;
    }

    public GestureContext Gesture3 {
        get => gesture3Property.Value;
        private set => gesture3Property.Value = value;
    }

    public GestureContext Gesture4 {
        get => gesture4Property.Value;
        private set => gesture4Property.Value = value;
    }

    private GestureContext Gesture(int index) {
        return index switch {
            0 => Gesture0,
            1 => Gesture1,
            2 => Gesture2,
            3 => Gesture3,
            4 => Gesture4,
            _ => null
        };
    }
}
using System.Collections;
using Slash.Unity.DataBind.Core.Data;
using Tempus.CoroutineTools;
using UnityEngine;

public class PlayerCharacterContext : Context {
    private readonly Property<float> timerFillProperty = new Property<float>();

    private readonly Property<Collection<bool>>
        activesProperty = new Property<Collection<bool>>(new Collection<bool>());

    private readonly Property<Collection<Sprite>> spritesProperty =
        new Property<Collection<Sprite>>(new Collection<Sprite>());

    public PlayerCharacterContext() {
        for (var i = 0; i < PlayerCharacterController.Instance.GestureCount; i++) {
            Actives.Add(false);
            Sprites.Add(null);
        }

        PlayerCharacterController.Instance.TimerFilled += fillAmount => TimerFill = fillAmount;
        PlayerCharacterController.Instance.GestureActiveSet += (index, active) => Actives[index] = active;
        PlayerCharacterController.Instance.GestureSpriteSet += (index, sprite) => Sprites[index] = sprite;
    }

    public float TimerFill {
        get => timerFillProperty.Value;
        private set => timerFillProperty.Value = value;
    }

    public Collection<bool> Actives => activesProperty.Value;
    public Collection<Sprite> Sprites => spritesProperty.Value;
}
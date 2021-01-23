using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerCharacterController : SingletonObject<PlayerCharacterController> {
    public event Action<float> TimerFilled;
    public event Action<int, bool> GestureActiveSet;
    public event Action<int, Sprite> GestureSpriteSet;
    public event Action SpecialSkillApplied;
    public event Action<GestureType> GestureDrawn;
    public event Action<GestureType> SkillDrawn;
    public event Action<float> SpecialSkillFailed;

    [SerializeField]
    private int gestureCount;

    [SerializeField]
    private int maxHp;

    [SerializeField]
    private int maxSp;

    [SerializeField]
    private float timerDuration;

    [SerializeField]
    private float moveSpeed;

    private bool isGracePeriod;
    private bool usingSpecialSkill;
    private bool protectionApplied;

    private int hp;
    private float moveSpeedMultiplier;
    private Tweener tweener;

    private readonly Queue<GestureType> gesturesToMatch = new Queue<GestureType>();

    public bool IsFullHp => hp >= maxHp;
    public int GestureCount => gestureCount;

    public int Sp { get; private set; }

    private void Awake() {
        hp = maxHp;
        Sp = maxSp;
    }

    public void ProcessGesture(GestureType gestureType) {
        if (usingSpecialSkill && gestureType == gesturesToMatch.Peek()) {
            GestureActiveSet?.Invoke(GestureCount - gesturesToMatch.Count, false);
            gesturesToMatch.Dequeue();

            if (gesturesToMatch.Count == 0) {
                usingSpecialSkill = false;
                tweener?.Kill();
                TimerFilled?.Invoke(0f);
                SpecialSkillApplied?.Invoke();
            }
        }

        if (gestureType >= GestureType.Circle) {
            if (usingSpecialSkill == false) {
                SkillDrawn?.Invoke(gestureType);
            }
        }
        else {
            GestureDrawn?.Invoke(gestureType);
            // TODO: 몬스터 제거 시 발동으로 바꿔야 함
            SpecialSkillLevel.UpdateLevel(1);
        }
    }

    public void GetDamaged(int amount) {
        if (protectionApplied) {
            protectionApplied = false;
        }
        else {
            hp = Mathf.Clamp(hp - amount, 0, hp);
        }
    }

    public void Heal(int amount) {
        hp = Mathf.Clamp(hp + amount, hp, maxHp);
    }

    public void ApplyProtection(int seconds = 0) {
        protectionApplied = true;

        if (seconds != 0) {
            DOVirtual.DelayedCall(seconds, () => protectionApplied = false);
        }
    }

    public void AdjustMoveSpeed(float multiplier, float seconds) {
        moveSpeedMultiplier = multiplier;
        DOVirtual.DelayedCall(seconds, () => moveSpeedMultiplier = 1f);
    }

    public void ApplyGracePeriod(int seconds) {
        isGracePeriod = true;
        DOVirtual.DelayedCall(seconds, () => isGracePeriod = false);
    }

    public void Foo() {
        var allGestures = GestureResources.Instance.AllGestures;
        var count = allGestures.Count();

        for (var i = 0; i < GestureCount; i++) {
            var gesture = allGestures.ElementAt(Random.Range(0, count - 1));
            gesturesToMatch.Enqueue(gesture);
            GestureActiveSet?.Invoke(i, true);
            GestureSpriteSet?.Invoke(i, GestureResources.Instance.Sprite(gesture));
            gesture.Log();
        }

        var fillAmount = 1f;
        usingSpecialSkill = true;

        tweener = DOTween.To(() => fillAmount, value => fillAmount = value, 0f, timerDuration)
            .SetEase(Ease.Linear)
            .OnUpdate(() => TimerFilled?.Invoke(fillAmount))
            .OnComplete(() => {
                usingSpecialSkill = false;
                FailurePenalty();

                for (var i = 0; i < GestureCount; i++) {
                    GestureActiveSet?.Invoke(i, false);
                }
            });
    }

    private void FailurePenalty() {
        if (Random.Range(0, 2) == 0) {
            hp = 50;
        }
        else {
            if (Sp == 0) {
                SpecialSkillFailed?.Invoke(3f);
            }
            else {
                Sp = 0;
            }
        }
    }
}
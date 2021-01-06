using System;
using System.Collections;
using System.Collections.Generic;
using Tempus.CoroutineTools;
using UnityEngine;

public class SkillHandler : SingletonObject<SkillHandler> {
    [SerializeField]
    private int specialSkillCooldown = 30;

    private readonly Dictionary<GestureType, ISkill> skills = new Dictionary<GestureType, ISkill>();
    private readonly Dictionary<int, ISkill> specialSkills = new Dictionary<int, ISkill>();

    private int specialSkillLevel;
    private int gestureUsageCount;
    private bool canActivateSpecialSkill = true;

    public int GestureCount => transform.GetChild(0).childCount;

    public void Activate(GestureType gestureType) {
        if (skills.TryGetValue(gestureType, out var skill)) {
            skill.Activate();
        }
    }

    public void ActivateSpecialSkill() {
        if (canActivateSpecialSkill == false || gestureUsageCount < 100) {
            gestureUsageCount++;
            return;
        }

        gestureUsageCount = 0;
        CooldownCoroutine().Start();
        PlayerCharacterController.Instance.ApplyGracePeriodCoroutine(2).Start();
    }

    private void ApplySpecialSkill() {
        for (var i = 0; i < specialSkillLevel; i++) {
            if (specialSkills.TryGetValue(i, out var skill)) {
                skill.Activate();
            }
        }
    }

    private IEnumerator CooldownCoroutine() {
        canActivateSpecialSkill = false;
        yield return Yield.Seconds(specialSkillCooldown);
        canActivateSpecialSkill = true;
    }

    private void Awake() {
        AddNormalSkill(new LightningSkill());
        AddNormalSkill(new HeartSkill());
        AddNormalSkill(new ClockSkill());
        AddNormalSkill(new StarSkill());

        AddSpecialSkill(new AdrenalineSkill());
        AddSpecialSkill(new SlaughterSkill());
        AddSpecialSkill(new AppleCareSkill());
        AddSpecialSkill(new AntiBossSkill());
    }

    private void AddNormalSkill(INormalSkill skill) {
        skills.Add(skill.GestureType, skill);
    }

    private void AddSpecialSkill(ISpecialSkill skill) {
        specialSkills.Add(skill.RequireLevel, skill);
    }
}
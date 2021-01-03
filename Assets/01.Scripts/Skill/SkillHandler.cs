using System;
using System.Collections;
using System.Collections.Generic;
using Tempus.CoroutineTools;
using UnityEngine;

public class SkillHandler : MonoBehaviour {
    [SerializeField]
    private int specialSkillCooldown = 30;

    private readonly Dictionary<GestureType, INormalSkill> skills = new Dictionary<GestureType, INormalSkill>();
    private readonly Dictionary<int, ISpecialSkill> specialSkills = new Dictionary<int, ISpecialSkill>();

    private int specialSkillLevel;
    private int gestureCount;
    private bool canActivateSpecialSkill = true;

    public void Activate(GestureType gestureType) {
        if (skills.TryGetValue(gestureType, out var skill)) {
            skill.Activate();
        }
    }

    public void ActivateSpecialSkill() {
        if (canActivateSpecialSkill == false || gestureCount < 100) {
            gestureCount++;
            return;
        }

        gestureCount = 0;
        StartCoroutine(Cooldown());
        // TODO: 특수 스킬 사용
        "특수 스킬 사용됨".Log();
    }

    private IEnumerator Cooldown() {
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
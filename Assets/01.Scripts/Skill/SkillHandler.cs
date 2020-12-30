using System.Collections.Generic;
using UnityEngine;

public class SkillHandler : MonoBehaviour {
    private readonly Dictionary<GestureType, ISkill> skills = new Dictionary<GestureType, ISkill>();

    public void Activate(GestureType gestureType) {
        skills[gestureType]?.Activate();
    }

    private void Awake() {
        AddSkillToDictionary(new LightningSkill());
        AddSkillToDictionary(new HeartSkill());
        AddSkillToDictionary(new ClockSkill());
        AddSkillToDictionary(new StarSkill());
    }

    private void AddSkillToDictionary(ISkill skill) {
        skills.Add(skill.GestureType, skill);
    }
}
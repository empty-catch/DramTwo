using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;

[Serializable]
public class MonsterInformation {
    [SerializeField]
    private int hp;
    
    [SerializeField]
    private float speed;
    
    [SerializeField]
    private float attackRange;

    [SerializeField]
    private float attackDamage;

    [SerializeField]
    private Sprite monsterSprite;
    public Sprite MonsterSprite => monsterSprite;

    [SerializeField]
    private MonsterAttackType attackType;
    public MonsterAttackType AttackType => attackType;

    public void Deconstruct(out int hp, out float speed, out float attackRange, out float attackDamage) {
        hp = this.hp;
        speed = this.speed;
        attackRange = this.attackRange;
        attackDamage = this.attackDamage;
    }
}

[CreateAssetMenu(fileName = "MonsterDataBase", menuName = "Scriptable Object/MonsterDatabase", order = 0)]
public class MonsterDataBase : ScriptableObject {
    #region Singleton

    private static MonsterDataBase instance;

    public static MonsterDataBase Instance {
        get {
            if (instance is null) {
                var prefab = Resources.Load<MonsterDataBase>("Unit/Resource/MonsterDataBase");
                if (prefab is null) {
                    string path = "Assets/Unit/Resource/MonsterDataBase";

                    var folderInfo = new DirectoryInfo(path);
                    
                    if (folderInfo.Exists == false) {
                        folderInfo.Create();
                    }

                    prefab = ScriptableObject.CreateInstance<MonsterDataBase>();
                    AssetDatabase.CreateAsset(prefab, $"{path}/MonsterDataBase.asset");
                }

                instance = prefab;
            }
            
            return instance;
        }
    }

    #endregion
    
    private Dictionary<MonsterType, MonsterInformation> monsterInformationDic = new Dictionary<MonsterType, MonsterInformation>();
    
    public MonsterInformation GetMonster(MonsterType monsterType) {
        if (monsterInformationDic.ContainsKey(monsterType) == false) {
            throw new ArgumentException();
        }

        return monsterInformationDic[monsterType];
    }
}
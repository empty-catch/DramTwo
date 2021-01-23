using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class MonsterGestureResource {
    [SerializeField]
    private GestureType name;
    public GestureType Name => name;
    
    [SerializeField]
    private Sprite gestureSprite;
    public Sprite GestureSprite => gestureSprite;
}

[CreateAssetMenu(fileName = "MonsterGestureResources", menuName = "Resources/MonsterGestureResources",order = 0)]
public class MonsterGestureResources : ScriptableObject {
    private static MonsterGestureResources instance;

    public static MonsterGestureResources Instance {
        get {
            if (instance is null) {
                var prefab = Resources.Load<MonsterGestureResources>("Unit/Resource/MonsterGestureSprite");
                if (prefab is null) {
                    string path = "Assets/Unit/Resource/MonsterGestureSprite";

                    var folderInfo = new DirectoryInfo(path);
                    
                    if (folderInfo.Exists == false) {
                        folderInfo.Create();
                    }

                    prefab = ScriptableObject.CreateInstance<MonsterGestureResources>();
                    AssetDatabase.CreateAsset(prefab, $"{path}/MonsterGestureResources.asset");
                }

                instance = prefab;
            }
            
            return instance;
        }
    }

    [Header("Gesture Items")]
    [SerializeField]
    private MonsterGestureResource[] monsterGestureResources;
    
    private Dictionary<GestureType, Sprite> gestureItems = new Dictionary<GestureType, Sprite>();
    public Dictionary<GestureType, Sprite> GestureItems => gestureItems;
    
    private void OnEnable() {
        foreach (var resourceItem in monsterGestureResources) {
            if (gestureItems.ContainsKey(resourceItem.Name) == false) {
                gestureItems.Add(resourceItem.Name ,resourceItem.GestureSprite);
            }
        }
    }
}
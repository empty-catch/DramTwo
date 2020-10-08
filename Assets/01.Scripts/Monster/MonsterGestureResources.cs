using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterGestureResource {
    // TODO : string enum으로 변경
    [SerializeField]
    private GestureType name;
    public GestureType Name => name;
    
    [SerializeField]
    private Sprite gestureSprite;
    public Sprite GestureSprite => gestureSprite;
}

[CreateAssetMenu(fileName = "MonsterGestureSprite", menuName = "Resources/MonsterGestureSprite",order = 0)]
public class MonsterGestureResources : ScriptableObject {
    private static string path = "/Unit/Resource/MonsterGestureSprite";
    private static MonsterGestureResources _instance;

    public static MonsterGestureResources instance {
        get {
            if (_instance == null) {
                var temp = Resources.Load<MonsterGestureResources>(path);
                if (temp == null) {
                    throw new Exception("Not Found Monster Gesture Sprite Resources.");
                }

                _instance = temp;
            }
            
            return _instance;
        }
    }

    [Header("Gesture Items")]
    [SerializeField]
    private MonsterGestureResource[] monsterGestureResources;
    
    // TODO : string enum으로 변경
    private Dictionary<GestureType, Sprite> gestureItems = new Dictionary<GestureType, Sprite>();
    public Dictionary<GestureType, Sprite> GestureItems => gestureItems;
    
    private void Awake() {
        foreach (var resourceItem in monsterGestureResources) {
            if (gestureItems.ContainsKey(resourceItem.Name) == false) {
                gestureItems.Add(resourceItem.Name ,resourceItem.GestureSprite);
            }
        }
    }
}
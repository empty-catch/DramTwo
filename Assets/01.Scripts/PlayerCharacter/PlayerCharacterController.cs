using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {
    private static PlayerCharacterController _instance;

    public static PlayerCharacterController instance {
        get {
            if (_instance == null) {
                var instanceObject = GameObject.FindObjectOfType<PlayerCharacterController>().GetComponent<PlayerCharacterController>();

                if (instanceObject == null) {
                    var prefabs = Resources.Load<GameObject>("/Unit/PlayerCharacter");
                    var playerCharacterObject = Instantiate(prefabs, Vector2.zero, Quaternion.identity);

                    instanceObject = playerCharacterObject.AddComponent<PlayerCharacterController>();
                }

                _instance = instanceObject;
            }
            
            return _instance;
        }
    }
}

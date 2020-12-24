using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {
    private static PlayerCharacterController instance;

    public static PlayerCharacterController Instance {
        get {
            instance ??= FindObjectOfType<PlayerCharacterController>();
            if (instance != null) {
                return instance;
            }

            var prefabs = Resources.Load<GameObject>("/Unit/PlayerCharacter");
            var playerCharacterObject = Instantiate(prefabs, Vector2.zero, Quaternion.identity);
            instance = playerCharacterObject.GetComponentSafe<PlayerCharacterController>();

            return instance;
        }
    }
}
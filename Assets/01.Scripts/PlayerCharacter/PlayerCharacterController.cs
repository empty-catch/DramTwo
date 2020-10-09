using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {
    // ReSharper disable once InconsistentNaming
    private static PlayerCharacterController _instance;

    // ReSharper disable once InconsistentNaming
    public static PlayerCharacterController instance {
        get {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<PlayerCharacterController>();

            if (_instance != null) return _instance;
            var prefabs = Resources.Load<GameObject>("/Unit/PlayerCharacter");
            var playerCharacterObject = Instantiate(prefabs, Vector2.zero, Quaternion.identity);
            _instance = playerCharacterObject.GetComponentSafe<PlayerCharacterController>();

            return _instance;
        }
    }
}
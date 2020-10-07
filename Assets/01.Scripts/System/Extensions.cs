using UnityEngine;

public static class Extensions {
    public static T GetComponentSafe<T>(this Transform transform) where T : MonoBehaviour {
        var component = transform.GetComponent<T>();
        if (component == null) {
            transform.gameObject.AddComponent<T>();
        }

        return component;
    }        
    
    public static T GetComponentSafe<T>(this GameObject gameObject) where T : MonoBehaviour {
        var component = gameObject.GetComponent<T>();
        if (component == null) {
            gameObject.AddComponent<T>();
        }

        return component;
    }    
}
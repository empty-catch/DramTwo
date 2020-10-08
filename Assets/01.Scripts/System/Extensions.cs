using DG.Tweening;
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

    // ReSharper disable once InconsistentNaming
    public static Tweener DOFade(this LineRenderer renderer, float endValue, float duration) {
        var ca = renderer.startColor;
        ca.a = endValue;
        var cb = renderer.endColor;
        cb.a = endValue;

        var startColor = new Color2(renderer.startColor, renderer.endColor);
        var endColor = new Color2(ca, cb);
        return renderer.DOColor(startColor, endColor, duration);
    }
}
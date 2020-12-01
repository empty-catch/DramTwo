using System;
using DG.Tweening;
using UnityEngine;

public static class Extensions {
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

    public static void SetColor(this LineRenderer renderer, Color color) {
        renderer.startColor = color;
        renderer.endColor = color;
    }
}
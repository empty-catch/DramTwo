using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using PDollarGestureRecognizer;

public class GestureDrawer : MonoBehaviour {
    [SerializeField]
    private new LineRenderer renderer;

    [SerializeField]
    private Image handle;

    [SerializeField]
    private float fadeDuration;

    [SerializeField]
    private float dotsDistance;

    [SerializeField]
    private TextAsset[] xmls;

    private readonly List<Point> points = new List<Point>();
    private new Camera camera;
    private Gesture[] gestures;
    private int positionCount;

    private Tweener rendererTweener;
    private Tweener handleTweener;

    private void Awake() {
        camera = Camera.main;
        gestures = new Gesture[xmls.Length];
        for (var i = 0; i < xmls.Length; i++) {
            gestures[i] = GestureIO.ReadGestureFromXML(xmls[i].text);
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ResetDrawer();
        }
        else if (Input.GetMouseButton(0)) {
            var position = Input.mousePosition;
            var worldPoint = camera.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
            handle.rectTransform.position = worldPoint;
            if (renderer.positionCount > 0 &&
                (worldPoint - renderer.GetPosition(positionCount - 1)).sqrMagnitude < dotsDistance) {
                return;
            }

            points.Add(new Point(position.x, -position.y, 0));
            positionCount++;
            renderer.positionCount = positionCount;
            renderer.SetPosition(positionCount - 1, worldPoint);

            var candidate = new Gesture(points.ToArray());
        }
        else if (Input.GetMouseButtonUp(0)) {
            // if (positionCount > 10 || positionCount > 1 &&
            //     (renderer.GetPosition(0) - renderer.GetPosition(positionCount - 1)).sqrMagnitude >= 0.5f) {
            //     var candidate = new Gesture(points.ToArray());
            //     var result = PointCloudRecognizer.Classify(candidate, gestures);
            // }

            FadeOut();
        }
    }

    private void ResetDrawer() {
        points.Clear();
        positionCount = 0;
        renderer.positionCount = 0;

        rendererTweener?.Kill();
        renderer.enabled = true;
        renderer.startColor = renderer.endColor = Color.white;

        handle.enabled = true;
        handleTweener?.Kill();
        handle.color = Color.white;
    }

    private void FadeOut() {
        rendererTweener = renderer.DOFade(0f, fadeDuration).OnComplete(() => renderer.enabled = false);
        handleTweener = handle.DOFade(0f, fadeDuration).OnComplete(() => handle.enabled = false);
    }
}
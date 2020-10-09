using System.Collections.Generic;
using System.Linq;
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
    private float drawerFadeDuration;

    [SerializeField]
    private GestureInfo[] gestureInfos;

    private readonly List<Point> points = new List<Point>();
    private new Camera camera;
    private Tweener tweener;
    private int positionCount;

    private IEnumerable<Gesture> Gestures => gestureInfos.Select(info => info.Gesture);

    private void Awake() {
        camera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ResetDrawer();
            var color = new Color2(Color.white, Color.white);
            tweener = renderer.DOColor(color, color, drawerFadeDuration).SetAutoKill(false);
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

            if (positionCount < 2) {
                return;
            }

            var candidate = new Gesture(points.ToArray());
            var result = PointCloudRecognizer.Classify(candidate, Gestures);
            ColorRenderer(result);
        }
        else if (Input.GetMouseButtonUp(0)) {
            tweener.Kill();
            FadeOut();
        }
    }

    private void ColorRenderer(Result result) {
        tweener.ChangeStartValue(new Color2(renderer.startColor, renderer.endColor));

        if (result.GestureClass == "No match" || result.Score < 0.5f) {
            tweener.ChangeEndValue(new Color2(Color.white, Color.white)).Restart();
        }
        else {
            var gestureInfo = gestureInfos.First(info => info.Type.ToString() == result.GestureClass);
            var color = Color.Lerp(Color.white, gestureInfo.Color, result.Score);
            tweener.ChangeEndValue(new Color2(color, color)).Restart();
        }
    }

    private void ResetDrawer() {
        points.Clear();
        positionCount = 0;
        renderer.positionCount = 0;

        renderer.DOKill();
        renderer.enabled = true;
        renderer.SetColor(Color.white);

        handle.DOKill();
        handle.enabled = true;
        handle.color = Color.white;
    }

    private void FadeOut() {
        renderer.DOFade(0f, fadeDuration).OnComplete(() => renderer.enabled = false);
        handle.DOFade(0f, fadeDuration).OnComplete(() => handle.enabled = false);
    }
}
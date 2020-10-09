using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using PDollarGestureRecognizer;
using UnityEngine.Events;

public class GestureDrawer : MonoBehaviour {
    [SerializeField]
    private new LineRenderer renderer;

    [SerializeField]
    private Image handle;

    [SerializeField]
    private ParticleSystem particle;

    [SerializeField]
    private float fadeDuration;

    [SerializeField]
    private float dotsDistance;

    [SerializeField]
    private float drawerFadeDuration;

    [SerializeField]
    private GestureInfo[] gestureInfos;

    [SerializeField]
    private UnityEvent<GestureType> onDrawn;

    private readonly List<Point> points = new List<Point>();
    private new Camera camera;
    private Tweener tweener;

    private GestureType gestureType;
    private Vector3 position;
    private Vector3 worldPosition;
    private int positionCount;

    private IEnumerable<Gesture> Gestures => gestureInfos.Select(info => info.Gesture);

    private void Awake() {
        camera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ResetDrawer();
            UpdatePositions();
            UpdateDrawer();
            ResetRenderer();

            var color = new Color2(Color.white, Color.white);
            tweener = renderer.DOColor(default, color, drawerFadeDuration).SetAutoKill(false);
        }
        else if (Input.GetMouseButton(0)) {
            UpdatePositions();
            if ((worldPosition - renderer.GetPosition(positionCount - 1)).sqrMagnitude < dotsDistance) {
                return;
            }

            UpdateDrawer();
            var candidate = new Gesture(points.ToArray());
            var result = PointCloudRecognizer.Classify(candidate, Gestures);
            ColorRenderer(result);
        }
        else if (Input.GetMouseButtonUp(0)) {
            onDrawn?.Invoke(gestureType);
            tweener.Kill();
            FadeOut();
        }
    }

    private void UpdatePositions() {
        position = Input.mousePosition;
        worldPosition = camera.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
        handle.rectTransform.position = worldPosition;
    }

    private void UpdateDrawer() {
        points.Add(new Point(position.x, -position.y, 0));
        positionCount++;
        renderer.positionCount = positionCount;
        renderer.SetPosition(positionCount - 1, worldPosition);
    }

    private void ColorRenderer(Result result) {
        tweener.ChangeStartValue(new Color2(renderer.startColor, renderer.endColor));
        if (result.GestureClass == "No match" || result.Score < 0.5f) {
            tweener.ChangeEndValue(new Color2(Color.white, Color.white)).Restart();
            gestureType = GestureType.None;
        }
        else {
            var gestureInfo = gestureInfos.First(info => info.Type.ToString() == result.GestureClass);
            var color = Color.Lerp(Color.white, gestureInfo.Color, result.Score);
            tweener.ChangeEndValue(new Color2(color, color)).Restart();
            gestureType = gestureInfo.Type;
        }
    }

    private void ResetDrawer() {
        points.Clear();
        positionCount = 0;
        renderer.positionCount = 0;
        gestureType = GestureType.None;
    }

    private void ResetRenderer() {
        renderer.DOKill();
        renderer.enabled = true;
        renderer.SetColor(Color.white);

        handle.DOKill();
        handle.enabled = true;
        handle.color = Color.white;
        particle.Play();
    }

    private void FadeOut() {
        renderer.DOFade(0f, fadeDuration).OnComplete(() => renderer.enabled = false);
        handle.DOFade(0f, fadeDuration).OnComplete(() => handle.enabled = false);
        particle.Stop();
    }
}
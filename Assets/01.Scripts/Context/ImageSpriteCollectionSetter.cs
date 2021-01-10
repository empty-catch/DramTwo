using Slash.Unity.DataBind.Core.Data;
using Slash.Unity.DataBind.Foundation.Setters;
using UnityEngine;
using UnityEngine.UI;

public class ImageSpriteCollectionSetter : ComponentSingleSetter<Image, Collection<Sprite>> {
    [SerializeField]
    private int index;

    protected override void UpdateTargetValue(Image target, Collection<Sprite> value) {
        if (target != null) {
            target.sprite = value[index];
        }
    }
}
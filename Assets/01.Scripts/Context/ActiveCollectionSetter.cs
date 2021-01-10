using Slash.Unity.DataBind.Core.Data;
using Slash.Unity.DataBind.Foundation.Setters;
using UnityEngine;

public class ActiveCollectionSetter : GameObjectSingleSetter<Collection<bool>> {
    [SerializeField]
    private int index;

    protected override void OnValueChanged(Collection<bool> newValue) {
        if (Target != null) {
            Target.SetActive(newValue[index]);
        }
    }
}
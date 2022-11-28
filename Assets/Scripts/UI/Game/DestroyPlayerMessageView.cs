using System;
using UI.Common;
using UnityEngine;

public class DestroyPlayerMessageView : MonoBehaviour
{
    [field: SerializeField] public ButtonView DestroyPlayerButton;

    public void Init(Action onClickAction)
    {
        DestroyPlayerButton.Init(onClickAction);
    }
}

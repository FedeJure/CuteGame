using System;
using Modules.ActorModule.Scripts.UnityDelivery.Skin;
using UnityEngine;
using UnityEngine.UI;

public class UnitySelectableSkinView : MonoBehaviour
{
    public event Action<ActorSkinData> OnClick = data => { };
    [SerializeField] private Button button;
    [SerializeField] private ActorSkinData skinData;
    [SerializeField] private Image image;

    private void Start()
    {
        image.material = skinData.material;
        button.onClick.AddListener(() => OnClick(skinData));
    }
}

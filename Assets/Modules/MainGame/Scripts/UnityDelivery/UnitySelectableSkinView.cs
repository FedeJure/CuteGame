﻿using System;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.UnityDelivery.Skin;
using UnityEngine;
using UnityEngine.UI;

public class UnitySelectableSkinView : MonoBehaviour
{
    public event Action<ActorSkinData> OnClick = data => { };
    [SerializeField] private Button button;
    private ActorSkinData skinData;
    [SerializeField] private Image image;
    
    public void Init(ActorSkinData data)
    {
        skinData = data;
        image.material = skinData.material;
        button.onClick.AddListener(() => OnClick(skinData));
    }
}

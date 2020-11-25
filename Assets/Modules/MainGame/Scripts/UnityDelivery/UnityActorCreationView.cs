using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.UnityDelivery.Skin;
using Modules.Common;
using Modules.MainGame.Scripts.Presentation;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UnityActorCreationView : MonoBehaviour
{
    public event Action<CreationData> OnCreate = data => { };
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] SkinnedMeshRenderer mesh;
    [SerializeField] ActorSkinData bodySkin;
    [SerializeField] ActorSkinData headSkin;
    [SerializeField] Button creationButton;
    [SerializeField] List<UnitySelectableSkinView> bodySkins;
    [SerializeField] List<UnitySelectableSkinView> headSkins;
    [SerializeField] RectTransform content;


    private void Start()
    {
        creationButton.onClick.AddListener(OnCreationButtonClicked);
        bodySkins.ForEach(skinView => skinView.OnClick += HandleBodySkinChange);
        headSkins.ForEach(skinView => skinView.OnClick += HandleHeadSkinChange);
        // HandleBodySkinChange(bodySkin);
        // HandleHeadSkinChange(headSkin);
    }

    private void OnEnable()
    {
        content.localPosition = new Vector3(0, -1500, 0);
        LeanTween.moveLocal(content.gameObject, new Vector3(0, -500, 0), 1)
            .setEaseOutQuad()
            .OnCompleteAsObservable()
            .Subscribe();
    }
    private void OnCreationButtonClicked()
    {
        if (nameInput.text.Length > 0)
        {
            OnCreate(new CreationData(nameInput.text, bodySkin, headSkin));
        }
    }

    private void HandleHeadSkinChange(ActorSkinData data)
    {
        bodySkin = data;
        mesh.materials = new []{ mesh.materials[0], data.material};
    }
    
    private void HandleBodySkinChange(ActorSkinData data)
    {
        headSkin = data;
        mesh.materials = new []{ data.material, mesh.materials[1]};
    }
}

using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.UnityDelivery.Skin;
using Modules.MainGame.Scripts.Presentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnityActorCreationView : MonoBehaviour
{
    public event Action<CreationData> OnCreate = data => { };
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] SkinnedMeshRenderer mesh;
    [SerializeField] ActorSkinData bodySkin;
    [SerializeField] ActorSkinData headSkin;
    [SerializeField] private Button creationButton;
    [SerializeField] private List<UnitySelectableSkinView> bodySkins;
    [SerializeField] private List<UnitySelectableSkinView> headSkins;


    private void Start()
    {
        creationButton.onClick.AddListener(OnCreationButtonClicked);
        bodySkins.ForEach(skinView => skinView.OnClick += HandleBodySkinChange);
        headSkins.ForEach(skinView => skinView.OnClick += HandleHeadSkinChange);
        HandleBodySkinChange(bodySkin);
        HandleHeadSkinChange(headSkin);
    }


    private void OnCreationButtonClicked()
    {
        if (nameInput.text.Length > 0)
        {
            OnCreate(new CreationData(nameInput.text, bodySkin, headSkin));
        }
    }

    private void HandleBodySkinChange(ActorSkinData data)
    {
        bodySkin = data;
        mesh.materials = new []{ mesh.materials[0], data.material};
    }
    
    private void HandleHeadSkinChange(ActorSkinData data)
    {
        headSkin = data;
        mesh.materials = new []{ data.material, mesh.materials[1]};
    }
}

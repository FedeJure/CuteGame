using System;
using System.Collections.Generic;
using System.Linq;
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
    private List<UnitySelectableSkinView> bodySkins = new List<UnitySelectableSkinView>();
    private List<UnitySelectableSkinView> headSkins = new List<UnitySelectableSkinView>();
    [SerializeField] RectTransform content;
    [SerializeField] private ActorSkinConfig config;
    [SerializeField] private UnitySelectableSkinView skinViewTemplate;
    [SerializeField] private GameObject bodySkinsContainer;
    [SerializeField] private GameObject creamSkinsContainer;


    private void Start()
    {
        InitSkinSelectables();
        creationButton.onClick.AddListener(OnCreationButtonClicked);
    }

    private void InitSkinSelectables()
    {
        config.skins.FindAll(skin => skin.type == SkinType.Body).ForEach(skin =>
            {
                var skinView = Instantiate(skinViewTemplate, bodySkinsContainer.transform);
                skinView.Init(skin);
                bodySkins.Add(skinView);
            });
        config.skins.FindAll(skin => skin.type == SkinType.Head).ForEach(skin =>
        {
            var skinView = Instantiate(skinViewTemplate, creamSkinsContainer.transform);
            skinView.Init(skin);
            headSkins.Add(skinView);
        });
        
        bodySkins.ForEach(skinView => skinView.OnClick += HandleBodySkinChange);
        headSkins.ForEach(skinView => skinView.OnClick += HandleHeadSkinChange);
    }

    private void OnEnable()
    {
        content.localPosition = new Vector3(0, -2000, 0);
        LeanTween.moveLocal(content.gameObject, new Vector3(0, -460, 0), 1)
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

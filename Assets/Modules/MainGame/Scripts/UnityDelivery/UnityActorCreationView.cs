using System;
using System.Collections.Generic;
using System.Linq;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Presentation;
using Modules.ActorModule.Scripts.UnityDelivery;
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
    [SerializeField] private UnityActorView actorView;
    [SerializeField] Skin bodySkin;
    [SerializeField] Skin headSkin;
    [SerializeField] Button creationButton;
    private List<UnitySelectableSkinView> bodySkins = new List<UnitySelectableSkinView>();
    private List<UnitySelectableSkinView> headSkins = new List<UnitySelectableSkinView>();
    [SerializeField] RectTransform content;
    [SerializeField] private ActorSkinConfig config;
    [SerializeField] private UnitySelectableSkinView skinViewTemplate;
    [SerializeField] private GameObject bodySkinsContainer;
    [SerializeField] private GameObject creamSkinsContainer;
    [SerializeField] private MyColorPicker colorPicker;
    [SerializeField] private Button openColorPicker;
    

    private UnitySelectableSkinView selectedSkinView;
    private Dictionary<string, Color?> lastColor = new Dictionary<string, Color?>();
    private ActorSkinData selectedSkin;

    private void Start()
    {
        InitSkinSelectables();
        creationButton.onClick.AddListener(OnCreationButtonClicked);
        openColorPicker.onClick.AddListener(OnOpenColorPicker);
    }

    private void OnOpenColorPicker()
    {
        colorPicker.Open()
            .Do(response =>
            {
                UpdateSkinColor(response.color);
            })
            .Last()
            .Do(response =>
            {
                if (response.ok)
                {
                    UpdateSkinColor(response.color);
                    lastColor.Add(selectedSkin.key, response.color);
                }
                else
                {
                   UpdateSkinColor(lastColor[selectedSkin.key]);
                }
            })
            .Subscribe();
    }

    private void UpdateSkinColor(Color? color)
    {
        if (!color.HasValue) return;
        selectedSkinView.SetColor(color.Value);
        switch (selectedSkin.type)
        {
            case SkinType.Body:
                bodySkin = new Skin(selectedSkin.key, color.Value);
                actorView.SetBodySkin(bodySkin);
                break;
            case SkinType.Head:
                headSkin = new Skin(selectedSkin.key, color.Value);
                actorView.SetHeadSkin(headSkin);
                break;
        }
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
        bodySkins.ForEach(skinView => skinView.OnClick += skin =>
        {
            HandleSkinClick(skin,skinView);
            HandleBodySkinChange(skin);
        });
        headSkins.ForEach(skinView => skinView.OnClick += skin =>
        {
            HandleSkinClick(skin, skinView);
            HandleHeadSkinChange(skin);
        });
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

    private void HandleBodySkinChange(ActorSkinData data)
    {
        bodySkin = new Skin(data.key);
        actorView.SetBodySkin(bodySkin);
    }
    
    private void HandleHeadSkinChange(ActorSkinData data)
    {
        headSkin = new Skin(data.key);
        actorView.SetHeadSkin(headSkin);
    }

    private void HandleSkinClick(ActorSkinData skin,UnitySelectableSkinView view)
    {
        openColorPicker.gameObject.SetActive(skin.acceptColor);
        selectedSkin = skin;
        if (selectedSkinView) selectedSkinView.SetSelected(false);
        view.SetSelected(true);
        selectedSkinView = view;
    }
}

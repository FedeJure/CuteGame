using System;
using HSVPicker;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerResponse
{
    public bool ok;
    public Color color;
    public ColorPickerResponse(bool ok, Color color)
    {
        this.ok = ok;
        this.color = color;
    }
    public ColorPickerResponse(Color color)
    {
        this.color = color;
    }
}

public class MyColorPicker : MonoBehaviour
{
    [SerializeField] private ColorPicker colorPicker;
    [SerializeField] private GameObject content;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;
    
    private Subject<Unit> onOk = new Subject<Unit>();
    private Subject<Unit> onCancel = new Subject<Unit>();
    private ReactiveProperty<Color> selectedColor = new ReactiveProperty<Color>( Color.white);

    private void Awake()
    {
        okButton.onClick.AddListener(() =>
        {
            onOk.OnNext(Unit.Default);
        });
        cancelButton.onClick.AddListener(() =>
        {
            onCancel.OnNext(Unit.Default);
        });
        colorPicker.onValueChanged.AddListener((color) => { selectedColor.Value = color; });
    }

    private void Start()
    {
        content.SetActive(false);
    }

    public IObservable<ColorPickerResponse> Open()
    {
        content.SetActive(true);
        var response = new Subject<ColorPickerResponse>(); 
        var colorSubscribe = selectedColor.Do(color =>
        {
            response.OnNext(new ColorPickerResponse(color));
        }).Subscribe();
        var okSubscribe = onOk.Do((_) =>
       {
           response.OnNext(new ColorPickerResponse(true, selectedColor.Value));
           response.OnCompleted();
       }).Subscribe();
        var cancelSubscribe = onCancel.Do((_) =>
        {
            response.OnNext(new ColorPickerResponse(false, selectedColor.Value));
            response.OnCompleted();

        }).Subscribe();
        return response.DoOnCompleted(() =>
        {
            content.SetActive(false);
            okSubscribe.Dispose();
            cancelSubscribe.Dispose();
            colorSubscribe.Dispose();
        });
    }
}

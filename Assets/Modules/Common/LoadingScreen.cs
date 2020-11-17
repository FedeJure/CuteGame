using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject screen;
    [SerializeField] private RectTransform loadingIcon;

    private void Start()
    {
        screen.SetActive(false);
    }

    public void StartLoading()
    {
        screen.SetActive(true);
        LeanTween.rotate(loadingIcon, 1000f, 1f)
            .setEaseInQuad()
            .setLoopPingPong(0);
    }

    public void StopLoading()
    {
        screen.SetActive(false);
    }
}

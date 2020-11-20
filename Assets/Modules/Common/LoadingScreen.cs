using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject screen;
    [SerializeField] private RectTransform loadingIcon;
    private LTDescr rotating;

    private void Awake()
    {
        gameObject.SetActive(true);
        screen.SetActive(false);
        rotating = LeanTween.rotateZ(loadingIcon.gameObject, 2000f, 1f)
            .setEaseInQuad()
            .setLoopPingPong(0);
        rotating.pause();
    }

    public void StartLoading()
    {
        screen.SetActive(true);
        rotating.resume();
    }

    public void StopLoading()
    {
        screen.SetActive(false);
        rotating.pause();
    }
}


using UnityEngine;

public class MainGameCamera : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera uiCamera;

    readonly int startMainGameKey = Animator.StringToHash("startGame");

    private void Start()
    {
    }

    public void ShowMainGame()
    {
        animator.SetTrigger(startMainGameKey);
    }
}

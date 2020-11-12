using UnityEngine;

public class RotatingDonut : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private RectTransform rectTransform;

    void Update()
    {
        transform.Rotate(new Vector3(0, 1 , 1), velocity * Time.deltaTime);
    }
}

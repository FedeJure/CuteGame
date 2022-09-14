using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderPosition : MonoBehaviour
{
    void Start()
    {
        var rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -Banner.size * 1.7f - 10);
        // rt.localPosition = new Vector3(r#ift.localPosition.x, -Banner.size, rt.localPosition.z);
    }
}

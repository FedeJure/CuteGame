using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -Banner.size * 1.7f - 10);
        // rt.localPosition = new Vector3(rt.localPosition.x, -Banner.size, rt.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

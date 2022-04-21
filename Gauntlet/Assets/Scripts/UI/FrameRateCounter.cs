using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FrameRateCounter : MonoBehaviour
{
    public Text frameText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameText.text = "FPS: " + (int)(1f / Time.unscaledDeltaTime);
    }
}

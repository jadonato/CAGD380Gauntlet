using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FrameRateCounter : MonoBehaviour
{
    public Text frameText;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            frameText.text = "FPS: " + (int)(1f / Time.unscaledDeltaTime);
            time = 1;
        }
        else
        {
            time -= 1 * Time.deltaTime;
        }
        
    }
}

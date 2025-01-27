using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
   

    // Update is called once per frame
    void Start()
    {
        int width = Mathf.RoundToInt(1920 * 1.5f);
        int height = Mathf.RoundToInt(1080 * 1.5f);
        Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
    
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

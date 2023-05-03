using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VipuHint : MonoBehaviour
{
    [SerializeField] private ShowGuidance guidance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("ShowHint", 180f);
    }

    private void ShowHint()
    {
        guidance.SetUpGuidance("What goes up? What goes down? Look at the images and try to think where you normally find these things.");
        Invoke("Close", 10f);
    }

    private void Close()
    {
        guidance.CloseGuidance();
    }
}



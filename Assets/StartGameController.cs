using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartGameController : MonoBehaviour
{
    [SerializeField] Image hanzeLogo;
    [SerializeField] Image bufferLogo;
    public Animation hanzeFade;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bufferLogo.enabled = false;
        PlayAnimation();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayAnimation()
    {
        hanzeFade.Play("Hanze_Fade");
        hanzeFade.IsDestroyed();
        hanzeLogo.enabled = false;

    }
}

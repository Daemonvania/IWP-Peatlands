using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Fade : MonoBehaviour
{
    [SerializeField] CanvasGroup myUIGroup;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    public float delay = 3f;
    private float lastCallTime;
    private bool isWaiting = false;
    public bool isInterreg = false;

    public void ShowUI()
    {
        fadeIn = true;
        isWaiting = true;
        lastCallTime = Time.time;
    }

    public void HideUI()
    {
        Debug.Log("HideUI is called");
        fadeOut = true;
        
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (myUIGroup.alpha < 1) {
                myUIGroup.alpha += Time.deltaTime;
                if(myUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {

            if (myUIGroup.alpha >= 0)
            {
                Debug.Log(myUIGroup.alpha + " this is the alpha");
                myUIGroup.alpha -= Time.deltaTime;
                if (myUIGroup.alpha == 0)
                {
                    fadeOut = false;
                    if (isInterreg)
                    {
                        SceneManager.LoadScene(1);
                    }
                }
            }
        }

        if (isWaiting && Time.time - lastCallTime >= delay)
        {
            HideUI();
            isWaiting = false;
            Debug.Log("Time has passed");
        }
    }

    private void OnEnable()
    {
        ShowUI();
        myUIGroup.alpha = 0;
        
    }

   
}

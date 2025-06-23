using UnityEngine;
using UnityEngine.UI;

public class StartGame_Controller : MonoBehaviour
{
    [SerializeField] public Image hanzeLogo;
    [SerializeField] public Image bufferLogo;

    public float delay = 3f;
    private float lastCallTime;
    private bool isWaiting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hanzeLogo.gameObject.SetActive(true);
        isWaiting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isWaiting && Time.time - lastCallTime >= delay)
        {
            bufferLogo.gameObject.SetActive(true);
            isWaiting = false;
        }
    }
}

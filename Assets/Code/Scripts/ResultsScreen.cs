using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text financialText;
    [SerializeField] private TMP_Text envText;
    
    [SerializeField] Slider financialSlider;
    [SerializeField] Slider envSlider;
    
    [SerializeField] private GameObject resultsPanel;
    
    private ManageCurrencies _manageCurrencies;
    
    private void Awake()
    {
        _manageCurrencies = GameObject.FindGameObjectWithTag("CurrencyManager").GetComponent<ManageCurrencies>();
    }
    
    public void ShowResults()
    {
        financialText.text = "Financial Score: " + _manageCurrencies.GetMoney();
        envText.text = "Environmental Score: " + _manageCurrencies.GetEnvironment();
        
        financialSlider.value = Mathf.Clamp01(_manageCurrencies.GetMoney() / 1000f);
        envSlider.value = Mathf.Clamp01(_manageCurrencies.GetEnvironment() / 600f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    
    public void ToggleVisibility()
    {
        if (resultsPanel.activeSelf)
        {
            resultsPanel.SetActive(false);
        }
        else
        {
            resultsPanel.SetActive(true);
        }
    }
    
}

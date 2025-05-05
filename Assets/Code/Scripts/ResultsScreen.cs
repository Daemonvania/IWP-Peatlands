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
}

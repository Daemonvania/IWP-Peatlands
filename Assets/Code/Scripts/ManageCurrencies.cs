using TMPro;
using UnityEngine;

public class ManageCurrencies : MonoBehaviour
{
    
    private int _moneyScore = 0;
    private int _envScore = 0;
    
    
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text envText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(int amount)
    {
        _moneyScore += amount;
        UpdateUI();
    }
    
    public void AddEnvironment(int amount)
    {
        _envScore += amount;
        UpdateUI();
    }
    
    public int GetMoney()
    {
        return _moneyScore;
    }
    
    public int GetEnvironment()
    {
        return _envScore;
    }
    
    public void UpdateUI()
    {
        moneyText.text = +_moneyScore + "<sprite=0>";
        envText.text = _envScore + "<sprite=1>";
    }
}

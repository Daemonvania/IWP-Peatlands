using UnityEngine;

public class ManageCurrencies : MonoBehaviour
{
    
    private int _moneyScore = 0;
    private int _envScore = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(int amount)
    {
        _moneyScore += amount;
    }
    
    public void AddEnvironment(int amount)
    {
        _envScore += amount;
    }
    
    public int GetMoney()
    {
        return _moneyScore;
    }
    
    public int GetEnvironment()
    {
        return _envScore;
    }
}

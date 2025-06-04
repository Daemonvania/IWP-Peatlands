using System;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject player;
    private TilePlacing _tilePlacing;
    [SerializeField] TMPro.TMP_Text currentPlayerText;
    
    [SerializeField] private GameObject tileSelectScreen;
    private BusinessModelToolTip _businessModelToolTip;
    
    private PlayerRoleAssigner _playerRoleAssigner;
    
    private ManageTurns _manageTurns;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _tilePlacing = player.GetComponent<TilePlacing>();
        _manageTurns = GetComponent<ManageTurns>();
        _businessModelToolTip = FindFirstObjectByType<BusinessModelToolTip>(FindObjectsInactive.Include);
        _playerRoleAssigner = GameObject.FindWithTag("PlayerNames").GetComponent<PlayerRoleAssigner>();
    }

    private void Start()
    {
        // tileSelectScreen.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        _tilePlacing.OnTurnStarted += OnTurnStarted;
        _tilePlacing.OnTurnEnded += OnTurnEnded;
    }
    
    private void OnDisable()
    {
        _tilePlacing.OnTurnStarted -= OnTurnStarted;
        _tilePlacing.OnTurnEnded -= OnTurnEnded;
    }

    private void OnTurnStarted(TileSO tile)
    {
        tileSelectScreen.SetActive(false);
        _businessModelToolTip.HideToolTip();
        
    }
    
    private void OnTurnEnded()
    {
        Debug.Log("Turn ended");
        ShowTileCards showTileCards = tileSelectScreen.GetComponent<ShowTileCards>();
        showTileCards.SetHandTiles(_manageTurns.GetCurrentPlayerHandTiles());
        showTileCards.ShowPlayerHand();
        
        string playername;
        switch (_manageTurns.currentPlayerIndex)
        {
            case 0:
                playername =  _playerRoleAssigner.playerNames[0] +": <sprite=5> Farmer";
                break;
            case 1:
                playername =_playerRoleAssigner.playerNames[1] +": <sprite=2> Policymaker";
                break;
            case 2:
                playername =_playerRoleAssigner.playerNames[2] +": <sprite=4> Chief Of Industry";
                break;
            case 3:
                playername =_playerRoleAssigner.playerNames[3] +": <sprite=3> Banker";
                break;
            default:
                playername = "Unknown";
                break;
        }
       
        int currentPlayerShow = _manageTurns.currentPlayerIndex + 1;
        currentPlayerText.text = playername;
        tileSelectScreen.SetActive(true);
    }
}

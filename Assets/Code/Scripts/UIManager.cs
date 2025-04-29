using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject player;
    private TilePlacing _tilePlacing;
    [SerializeField] TMPro.TMP_Text currentPlayerText;
    
    [SerializeField] private GameObject tileSelectScreen;

    
    private ManageTurns _manageTurns;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _tilePlacing = player.GetComponent<TilePlacing>();
        _manageTurns = GetComponent<ManageTurns>();
    }

    private void Start()
    {
        tileSelectScreen.SetActive(false);
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
        int currentPlayerShow = _manageTurns.currentPlayerIndex + 1;
        currentPlayerText.text = "Player " + currentPlayerShow;
    }
    
    private void OnTurnEnded()
    {
        ShowTileCards showTileCards = tileSelectScreen.GetComponent<ShowTileCards>();
        showTileCards.SetHandTiles(_manageTurns.GetCurrentPlayerHandTiles());
        showTileCards.ShowPlayerHand();
        tileSelectScreen.SetActive(true);
    }
}

using System;
using UnityEngine;

public class ManageTurns : MonoBehaviour
{
    private GameObject player;
    private TilePlacing _tilePlacing;
    
    [HideInInspector] public int currentPlayerIndex = 4;
    
    public PlayerCardManager[] players;
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentPlayerIndex = 3;
        player = GameObject.FindGameObjectWithTag("Player");
        _tilePlacing = player.GetComponent<TilePlacing>();
    }

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

    private void Start()
    {
       
    }

    private void OnTurnStarted(TileSO tile)
    {
        players[currentPlayerIndex].ReplaceTile(tile);
    }
    private void OnTurnEnded()
    {
        currentPlayerIndex++;
        
        if (currentPlayerIndex >= 4)
        {
            currentPlayerIndex = 0;
        }
       
    }
    
    
    public TileSO[] GetCurrentPlayerHandTiles()
    {
        return players[currentPlayerIndex].GetHandTiles();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

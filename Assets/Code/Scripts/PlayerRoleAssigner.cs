using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
public class PlayerRoleAssigner : MonoBehaviour
{
    [Header("UI Elements")]
    public  TMP_InputField[] inputFields; // Drag 4 InputFields here in Inspector
    public Button continueButton;
    public TMP_Text resultText;
    public Button continueToSceneButton;
    public GameObject nameInputPanel;
    public GameObject roleAssignmentPanel;

    private string[] roles = { "Farmer", "Policymaker", "Chief of Industry", "Banker" };
    public List<string> playerNames = new List<string>();

    bool rolesAssigned = false;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        // Initial UI setup
        nameInputPanel.SetActive(true);
        roleAssignmentPanel.SetActive(false);
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        continueToSceneButton.onClick.AddListener(OnContinueToSceneClicked);
    }

    public void OnContinueButtonClicked()
    {
        if (rolesAssigned)
        {
            Debug.Log("Roles already assigned, cannot continue.");
            return;
        }
        rolesAssigned = true;
        continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        Debug.Log("Naming");
        // Collect player names
        playerNames.Clear();
        foreach (TMP_InputField input in inputFields)
        {
            if (!string.IsNullOrWhiteSpace(input.text))
            {
                playerNames.Add(input.text.Trim());
            }
        }

        if (playerNames.Count != 4)
        {
            resultText.text = "Please enter names for all 4 players!";
            return;
        }
        List<string> availableRoles = new List<string>(roles);
        Dictionary<string, string> assignedRoles = new Dictionary<string, string>();

        // // Assign roles randomly
        //
        // System.Random rand = new System.Random();
        // foreach (string player in playerNames)
        // {
        //     int index = rand.Next(availableRoles.Count);
        //     assignedRoles[player] = availableRoles[index];
        //     availableRoles.RemoveAt(index);
        // }

        for (int i = 0; i < playerNames.Count; i++)
        {
            assignedRoles[playerNames[i]] = availableRoles[i];
        }
        
        // Display results
        string resultString = "\n";
        foreach (var pair in assignedRoles)
        {
            resultString += $"{pair.Key}: {pair.Value}\n";
        }
        resultString += "\nPlayers can now pick up their role cards.";

        // Update UI
        nameInputPanel.SetActive(false);
        roleAssignmentPanel.SetActive(true);
        resultText.text = resultString;
    }

    public void OnContinueToSceneClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
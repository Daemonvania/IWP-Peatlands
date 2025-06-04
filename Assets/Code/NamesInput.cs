using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerRoleAssigner : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField[] nameInputs; // Drag 4 InputFields here in Inspector
    public Button continueButton;
    public Text resultText;
    public Button continueToSceneButton;
    public GameObject nameInputPanel;
    public GameObject roleAssignmentPanel;

    private string[] roles = { "Farmer", "Policymaker", "Chief of Industry", "Banker" };
    private List<string> playerNames = new List<string>();

    private void Start()
    {
        // Initial UI setup
        nameInputPanel.SetActive(true);
        roleAssignmentPanel.SetActive(false);
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        continueToSceneButton.onClick.AddListener(OnContinueToSceneClicked);
    }

    public void OnContinueButtonClicked()
    {
        // Collect player names
        playerNames.Clear();
        foreach (InputField input in nameInputs)
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

        // Assign roles randomly
        List<string> availableRoles = new List<string>(roles)
        Dictionary<string, string> assignedRoles = new Dictionary<string, string>();

        System.Random rand = new System.Random();
        foreach (string player in playerNames)
        {
            int index = rand.Next(availableRoles.Count);
            assignedRoles[player] = availableRoles[index];
            availableRoles.RemoveAt(index);
        }

        // Display results
        string resultString = "Roles Assigned:\n";
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
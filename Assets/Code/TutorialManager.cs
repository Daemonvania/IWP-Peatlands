using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [TextArea] public string message;
        public GameObject highlightObject; // Optional: highlight objects like buttons
    }

    public TutorialStep[] steps;
    public GameObject tutorialPanel;
    public TMPro.TextMeshProUGUI tutorialText; // or TMPro.TextMeshProUGUI
    public Button nextButton;
    public Button previousButton;

    private int currentStep = 0;

    void Start()
    {
        ShowStep(currentStep);
        nextButton.onClick.AddListener(NextStep);
        previousButton.gameObject.SetActive(false);
    }

    void ShowStep(int index)
    {
        tutorialPanel.SetActive(true);
        tutorialText.text = steps[index].message;

        foreach (var step in steps)
        {
            if (step.highlightObject != null)
                step.highlightObject.SetActive(false);
        }

        if (steps[index].highlightObject != null)
            steps[index].highlightObject.SetActive(true);
    }

    public void NextStep()
    {
        currentStep++;
        if (currentStep < steps.Length)
        {
            ShowStep(currentStep);
            previousButton.gameObject.SetActive(true);
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void PreviousStep()
    {
        currentStep--;
        if (currentStep > -1)
        {
            ShowStep(currentStep);
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;
using Button = UnityEngine.UI.Button;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI continueButtonText;
    [SerializeField] private Button continueButton;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("X")==GameManager.StartLocation.x&&PlayerPrefs.GetFloat("Y")==GameManager.StartLocation.y)
        {
            continueButtonText.color=Color.gray;
            continueButton.interactable = false;
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        GameManager.Index = 0;
    }

    public void ContinueGame()
    {
        GameManager.Index = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiControls : MonoBehaviour
{
    [SerializeField] public GameObject panelLost;
    [SerializeField] public GameObject panelWon;
    [SerializeField] public GameObject panelMainMenu;
    [SerializeField] private TextMeshProUGUI textMoneyCounter;
    [SerializeField] private TextMeshProUGUI textGoldCounter;

    private void Start()
    {
        panelMainMenu.SetActive(true);
        panelLost.SetActive(false);
        panelWon.SetActive(false);
    }

    public void UpdateMoneyText(int newMoneyCount)
    {
        textMoneyCounter.text = newMoneyCount.ToString();
    }

    public void UpdateGoldText(int newGoldCount)
    {
        textGoldCounter.text = newGoldCount.ToString();
    }

    public void ShowPanelLost()
    {
        panelLost.SetActive(true);
    }

    public void StartGame()
    {
        panelMainMenu.SetActive(false);
    }

    public void ShowPanelWon()
    {
        panelWon.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
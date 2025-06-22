using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiControls : MonoBehaviour
{
    [SerializeField] public GameObject panelLost;
    [SerializeField] public GameObject panelWon;
    [SerializeField] public GameObject panelMainMenu;
    [SerializeField] public TextMeshProUGUI textMoneyCounter;
    [SerializeField] public TextMeshProUGUI textGoldCounter;
    [SerializeField] private TimerScript tc;

    private void Start()
    {
        panelMainMenu.SetActive(true);
        panelLost.SetActive(false);
        panelWon.SetActive(false);
    }

    public void UpdateMoneyText(int newMoneyCount)
    {
        textMoneyCounter.text = newMoneyCount + "/18 Money Piles".ToString();
    }

    public void UpdateGoldText(int newGoldCount)
    {
        textGoldCounter.text = newGoldCount + "/3 Gold Bars (need 3 to win)".ToString();
    }

    public void ShowPanelLost()
    {
        panelLost.SetActive(true);
    }

    public void StartGame()
    {
        panelMainMenu.SetActive(false);
        tc.StartTimer();
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
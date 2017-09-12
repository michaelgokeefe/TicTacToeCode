using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;

    private int moveCount = 9;

    private string playerSide;

    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
        gameOverPanel.SetActive(false);
    }
    
    public void RestartGame()
    {
        playerSide = "X";
        moveCount = 9;
        SetButtonTextNothing();
        SetBoardInteractable(true);
        gameOverPanel.SetActive(false);
    }
    
    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount--;
        if (CheckWin())
        {
            SetGameOverText(playerSide + " Won!");
        }
        else
        {
            if (moveCount <= 0)
            {
                SetGameOverText("Draw!");
            }
            else
            {
                ChangeSides();
            }
        }
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    void SetGameOverText(string text)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = text;
        SetBoardInteractable(false);
    }
    
    private void SetGameControllerReferenceOnButtons()
    {
        foreach (Text buttonText in buttonList)
        {
            buttonText.GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    private void SetButtonTextNothing()
    {
        foreach (Text buttonText in buttonList)
        {
            buttonText.text = "";
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        foreach (Text buttonText in buttonList)
        {
            buttonText.GetComponentInParent<Button>().interactable = toggle;
        }
    }

    private bool CheckWin()
    {
        if (CheckRow(0) || CheckRow(3) || CheckRow(6))
        {
            return true;
        }
        else if (CheckColumn(0) || CheckColumn(1) || CheckColumn(2))
        {
            return true;
        }
        else if (CheckForwardDiagonal() || CheckBackwardDiagonal())
        {
            return true;
        }
        return false;
    }

    private bool CheckRow(int start)
    {
        for (int i = start; i < start + 3; i++)
        {
            if (buttonList[i].text != playerSide)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckColumn(int start)
    {
        for (int i = start; i < buttonList.Length; i += 3)
        {
            if (buttonList[i].text != playerSide)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckForwardDiagonal()
    {
        return buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide;
    }

    private bool CheckBackwardDiagonal()
    {
        return buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class creates a cheat menu to observe data
/// </summary>
public class CheatMenu : MonoBehaviour, ICheatMenu
{
    private string[] _cheatCode;
    private int _index;
    private bool _cheat;
    private GameController _prizeData;
    private CanvasGroup _panelCheatMenu;
    private Text _jarsAmt;
    private Text _noWinAmt;
    private Text _poundAmt;

    private void Start()
    {
        _panelCheatMenu = GameObject.Find("PanelCheatMenu").GetComponent<CanvasGroup>();
        _prizeData = GameObject.Find("GameController").GetComponent<GameController>();
        _jarsAmt = GameObject.Find("amount1").GetComponent<Text>();
        _noWinAmt = GameObject.Find("amount2").GetComponent<Text>();
        _poundAmt = GameObject.Find("amount3").GetComponent<Text>();
        _cheatCode = new string[] { "n", "2", "o" };
        _index = 0;

        //var gc = gameController.GetComponent<GameController>();
    }

    public void OpenCheatMenu()
    {
        _panelCheatMenu.alpha = 1;
    }

    public void CloseMenu()
    {
        _panelCheatMenu.alpha = 0;
        _cheat = false;
        _index = 0;
    }

    public void RemovePound()
    {
        _prizeData.m_PrizeData.POUND500 = 0;
    }

    public void ResetData()
    {
        _prizeData.m_SectorsAngles = new float[] { 78.85f, 53.4f, 27, -0.3f, -26.5f, -54.23f, -80.7f };
        _prizeData.m_PrizeData.JarsOfCoffee = 128;
        _prizeData.m_PrizeData.NoWin = 62;
        _prizeData.m_PrizeData.POUND500 = 1;    
    }

    void Update()
    {
        _jarsAmt.text = _prizeData.m_PrizeData.JarsOfCoffee.ToString();
        _noWinAmt.text = _prizeData.m_PrizeData.NoWin.ToString();
        _poundAmt.text = _prizeData.m_PrizeData.POUND500.ToString();

        if (_cheat == true)
        {
            OpenCheatMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(_cheatCode[_index]))
            {
                // right input, check next digit
                _index++;
            }
            else
            {
                // wrong input, restart from index 0
                _index = 0;
            }
        }

        if (_index == _cheatCode.Length)
        {
            _cheat = true;
            _index = 0;
        }
    }
}
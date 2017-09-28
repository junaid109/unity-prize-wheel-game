using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Button handles clicks from external button
/// </summary>]
///
[ExecuteInEditMode]
public class ButtonHandler : MonoBehaviour, IButtonHandler
{
    public Animator introAnim;
    public CanvasGroup arrow;

    private GameController m_GameController;
    private bool m_IsClicked;

    private void Start()
    {
        m_IsClicked = false;
        m_GameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if (ButtonData.clickCount > 150)
        {
            Debug.Log("150 clicks reach" + "\nyou have reached the limit for today");
            m_IsClicked = true;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button15) || Input.GetKeyDown(KeyCode.Joystick2Button15) || Input.GetKeyDown(KeyCode.Joystick3Button15) || Input.GetKeyDown(KeyCode.Joystick4Button15) || Input.GetKeyDown(KeyCode.P) && m_IsClicked == false && ButtonData.clickCount < 150)
        {
            m_IsClicked = true;
            HandleClick();
        }
    }

    /// <summary>
    /// Handle click turns wheel, increments clicker count and 
    /// passes it into check for prize
    /// </summary>
    public void HandleClick()
    {
        //introAnim.SetTrigger("animAppear");
        arrow.alpha = 1;
        ButtonData.clickCount++;
        m_GameController.TurnWheel();
        m_GameController.CheckForPrize(ButtonData.clickCount);
        Debug.Log("Button has been clicked " + ButtonData.clickCount + "of times");
        StartCoroutine("Delay");
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(9f);
        m_IsClicked = false;
        yield return new WaitForSeconds(1f);
    }
}
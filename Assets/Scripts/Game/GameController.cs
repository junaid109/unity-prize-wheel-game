using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls start and stop of prize wheel 
/// as well as the prize thats given away
/// </summary>
public class GameController : MonoBehaviour, IGameController
{
    #region public fields
    public GameObject circle;
    public CanvasGroup arrow;
    public Animator animJar;
    public Animator animPound;
    public PrizeData m_PrizeData;
    public Animator animNoWin;
    public Button turnButton;
    public float[] m_SectorsAngles = new float[] { 78.85f, 53.4f, 27, -0.3f, -26.5f, -54.23f, -80.7f };
    public int jarsOfCoffee = 128;
    public int noWin = 62;
    public int pound500 = 1;
    #endregion

    #region private fields
    private LightController m_LightController;
    private DataController m_DataController;
    private ButtonHandler m_ButtonHandler;
    private ButtonData m_ButtonData;
    private string currentLevel = "main";
    private string poundChecker = "pound";
    private bool m_IsStarted;
    private float m_FinalAngle;
    private float m_TmpAngle;
    private float tmpBefore;
    private float m_PoundAngle = -0.3f;
    private float m_StartAngle = 0;
    private float m_CurrentLerpRotationTime;
    private Text m_PrizeText;
    private DateTime currentTime = DateTime.Now;

    #endregion

    private void Start()
    {

        m_LightController = new LightController();
        m_ButtonData = new ButtonData();
        m_PrizeData = new PrizeData();
        m_PrizeData.JarsOfCoffee = jarsOfCoffee;
        m_PrizeData.NoWin = noWin;
        m_PrizeData.POUND500 = pound500;
        m_ButtonHandler = FindObjectOfType<ButtonHandler>().GetComponent<ButtonHandler>();
        m_PrizeText = GameObject.FindGameObjectWithTag("PrizeText").GetComponent<Text>();
        //circle = GameObject.FindGameObjectWithTag("Circle").GetComponent<GameObject>();
        m_LightController.LightOff();
        PlayerPrefs.SetInt("session1", 0);
        //PoundChecker();
        //CheckForCurrentDate();
    }

    //private void PoundChecker()
    //{
    //    if (PlayerPrefs.GetInt(poundChecker) == 0 && PlayerPrefs.GetInt(poundChecker) != 1)
    //    {
    //        Debug.Log("Pound is present");
    //        m_PrizeData.POUND500 = 1;
    //    }

    //    else if (PlayerPrefs.GetInt(poundChecker) == 1)
    //    {
    //        Debug.Log("Pound is not present");
    //        m_PrizeData.POUND500 = 0;
    //    }
    //}

    //private void CheckForCurrentDate()
    //{
    //    if (PlayerPrefs.GetInt(currentLevel) == 0 && PlayerPrefs.GetInt(currentLevel) != 1)
    //    {
    //        Debug.Log("current scene first time playing");
    //        PlayerPrefs.SetInt(currentLevel, 1);
    //    }
    //    else
    //    {
    //        Debug.Log("second time playing scene");      
    //        Debug.Log(PlayerPrefs.GetInt(currentLevel));
    //    }
    //}

    private void Update()
    {
        if (m_IsStarted || ButtonData.clickCount <= m_ButtonData.MAX_CLICKCOUNT)
        {
            m_PrizeText.text = "";
            turnButton.interactable = false;
            turnButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            turnButton.interactable = true;
            turnButton.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        if (!m_IsStarted)
            return;

        float maxLerpRotationTime = 4f;

        // increment timer once per frame
        m_CurrentLerpRotationTime += Time.deltaTime;
        if (m_CurrentLerpRotationTime > maxLerpRotationTime || circle.transform.eulerAngles.z == m_FinalAngle)
        {
            m_CurrentLerpRotationTime = maxLerpRotationTime;
            m_IsStarted = false;
            m_StartAngle = m_FinalAngle % 360;
        }

        // Calculate current position using linear interpolation
        float t = m_CurrentLerpRotationTime / maxLerpRotationTime;

        // This formulae allows to speed up at start and speed down at the end of rotation.
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(m_StartAngle, m_FinalAngle, t);
        circle.transform.eulerAngles = new Vector3(0, 0, angle);

    }

    public void ShowPrizesRemaining()
    {
        Debug.Log("jars of coffee left: " + m_PrizeData.JarsOfCoffee);
        Debug.Log("No Wins Left: " + m_PrizeData.NoWin);
        Debug.Log("500 pound left: " + m_PrizeData.POUND500);
    }

    /// <summary>
    /// When app quits we turn of the lights
    /// </summary>
    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("session1", 1);
        m_LightController.LightOff();
    }

    /// <summary>
    /// This method turns wheel game object and we set the angle points for our reference
    /// </summary>
    public void TurnWheel()
    {
        m_PoundAngle = -0.3f;
        m_CurrentLerpRotationTime = 0f;
        m_LightController.LightOff();

        // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
        //m_SectorsAngles = new float[] { 78.85f, 53.4f, 27, -26.5f, -54.23f, -80.7f };     
        int fullCircles = 5;
        float randomFinalAngle = m_SectorsAngles[UnityEngine.Random.Range(0, m_SectorsAngles.Length)];
        tmpBefore = randomFinalAngle;
        m_TmpAngle = tmpBefore;

        if (currentTime.Hour >= 17 && currentTime.Minute > 01 && m_PrizeData.POUND500 > 0 )
        {
            m_PoundAngle = tmpBefore;
            poundPrize();
        }
     
        CheckWheelFinalStop(tmpBefore);
        // Here we set up how many circles our wheel should rotate before stop
        m_FinalAngle = -(fullCircles * 360 + randomFinalAngle);
        m_IsStarted = true;
        //Debug.Log(tmpBefore);      
    }

 

    /// <summary>
    /// This method checks the angle of the final stop then returns 
    /// to GiveAwardIfAvailable()
    /// </summary>
    /// <param name="randomFinalAngle"></param>
    /// <returns></returns>
    public float CheckWheelFinalStop(float randomFinalAngle)
    {
        Debug.Log(randomFinalAngle);

        if (randomFinalAngle == 78.85f || randomFinalAngle == -80.7f)
        {
            Debug.Log("Final Angle is: " + randomFinalAngle);
        }

        if (randomFinalAngle == 53.4f || randomFinalAngle == -54.23f)
        {
            Debug.Log("Final Angle is: " + randomFinalAngle);
        }

        if (randomFinalAngle == 27 || randomFinalAngle == -26.5f)
        {
            Debug.Log("Final Angle is: " + randomFinalAngle);
        }

        if (randomFinalAngle == -0.3f)
        {
            Debug.Log("Final Angle is: " + randomFinalAngle);
        }

        return randomFinalAngle;
    }

    /// <summary>
    /// This method takes in the click number and checks if in range of 150
    /// </summary>
    /// <param name="clickNum"></param>
    public void CheckForPrize(int clickNum)
    {
        var clickCount = m_ButtonData.MAX_CLICKCOUNT - ButtonData.clickCount;

        var randomNum = UnityEngine.Random.Range(1, 150);

        Debug.Log(clickCount);
        Debug.Log(randomNum);

        if (randomNum > 0 && randomNum < 150 && clickCount != 0)
        {
            GiveAwardIfAvailable(randomNum);
        }
    }

    /// <summary>
    /// This is final method that determines the prize to give away to user
    /// it takes a random number that is between the number if clicks made from phsyical btn
    /// </summary>
    /// <param name="randomNum"></param>
    public void GiveAwardIfAvailable(int randomNum)
    {
       // int localRand = UnityEngine.Random.Range(1, 3);
        if (m_TmpAngle == 78.85f || m_TmpAngle == -80.7f || m_TmpAngle == 27 || m_TmpAngle == -26.5f)
        {
            jarsOfCoffeePrize();
        }

        if ( m_PrizeData.NoWin > 0 && m_TmpAngle == 53.4f || m_TmpAngle == -54.23f)
        {
            noWinPrize();
        }

        if (m_PrizeData.POUND500 > 0 && m_TmpAngle == -0.3f)
        {
            poundPrize();
        }

        if (currentTime.Hour >= 17 && currentTime.Minute > 01 && m_PrizeData.POUND500 > 0)
        {
            poundPrize();
        }

        else if (randomNum > 150)
        {
            noWinPrize();
            Debug.Log("else called");
        }
    }

    public void jarsOfCoffeePrize()
    {
        try
        {
            if (m_PrizeData.JarsOfCoffee > 0)
            {
                m_PrizeText.text = "Jar of Coffee";
                m_PrizeData.JarsOfCoffee--;
                StartCoroutine("DelayForJar");
            }
            else if (m_PrizeData.JarsOfCoffee < 0)
            {
                noWinPrize();
            }
        } catch (Exception e)
        {
            Debug.Log("Error running method " + e);
        }
        }

    public void noWinPrize()
    {
        try
        {
            if (m_PrizeData.NoWin > 0)
            {
                m_SectorsAngles[3] = -26.5f;

                m_PrizeText.text = "No Win";
                m_PrizeData.NoWin--;
                StartCoroutine("DelayForNoWin");
            }
            else if (m_PrizeData.NoWin < 0)
            {
                noWinPrize();
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error running method " + e);
        }
    }

    public void poundPrize()
    {
        try
        {
            if (m_PrizeData.POUND500 > 0)
            {
                m_SectorsAngles[3] = -26.5f;
                Debug.Log("removed 500 pnd from list " + m_SectorsAngles[3]);

                m_PrizeText.text = "£500";
                m_PrizeData.POUND500--;
                StartCoroutine("DelayForPound");
            }
            else if (m_PrizeData.POUND500 < 0)
            {
                noWinPrize();
            }
        } catch (Exception e)
        {
            Debug.Log("Error running method " + e);
        }
    }

    #region co-routines for delays
    private IEnumerator DelayForNoWin()
    {        
        yield return new WaitForSeconds(2);
        Debug.Log("coroutine called");
        yield return new WaitForSeconds(2);
        m_LightController.LightOff();
       // arrow.alpha = 0f;
        animNoWin.SetTrigger("animAppear");
    }

    private IEnumerator DelayForJar()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("coroutine called");
        yield return new WaitForSeconds(2);
        animJar.SetTrigger("animAppear");
        yield return new WaitForSeconds(1);
        m_LightController.LightUp();
    }

    private IEnumerator DelayForPound()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("coroutine called");
        yield return new WaitForSeconds(2);
        animPound.SetTrigger("animAppear");
        yield return new WaitForSeconds(1);
        m_LightController.LightUp();
    }
    #endregion
}
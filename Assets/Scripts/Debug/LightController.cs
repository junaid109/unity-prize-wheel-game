using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// This class controls the on/off behaviour of the lights
/// </summary>
public class LightController : MonoBehaviour
{
    [DllImport("mb", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern bool InitMbee();

    [DllImport("mb", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern bool SetOutputs(int outputs);

    void Start()
    {
        try
        {
            InitMbee();
            Debug.Log(InitMbee());
        }
        catch (Exception e)
        {
            Debug.Log("There was an error running the InitMbee: " + e);
        }
    }

    /// <summary>
    /// Lights turn on pin 13
    /// </summary>
    public void LightUp()
    {
        try
        {
            SetOutputs(13);
        }
        catch (Exception e)
        {
            Debug.Log("Could not activate the lights ERROR: " + e);
        }
        // StartCoroutine("Delay");
    }

    /// <summary>
    /// disable lights
    /// </summary>
    public void LightOff()
    {
        try
        {
            SetOutputs(0);
        }
        catch (Exception e)
        {
            Debug.Log("Could not turn off the lights ERROR: " + e);
        }
    }

    /// <summary>
    /// Co-routine method
    /// </summary>
    /// <returns></returns>
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(8);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the different dates/time that the 
/// prize can be given throughout the dates
/// </summary>
public class TimeController : MonoBehaviour {

    private static DateTime timeToWinPounds = new DateTime(2017, 5, 3, 16, 7, 0);
    private static DateTime activityDay1= DateTime.Now;

    // Use this for initialization
    void Start () {
        if (timeToWinPounds == activityDay1)
        {

        }	
	}
}
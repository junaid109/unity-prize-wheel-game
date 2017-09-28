using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Prize Data contains fields for prize data
/// </summary>
[Serializable]
public class PrizeData
{
    private int jarsOfCoffee;
    private int noWin;
    private int pound500;

    private readonly int MAX_JARS_OF_COFFEE = 128;
    private readonly int MAX_NO_WIN = 62;
    private readonly int MAX_POUND500 = 1;

    public int JarsOfCoffee
    {
        get
        {
            return jarsOfCoffee;
        }

        set
        {
            if(value <= 0)
            {
                Debug.Log("no jars of coffee left" + value);
                value = 0;
            }
            jarsOfCoffee = value;
        }
    }

    public int NoWin
    {
        get
        {
            return noWin;
        }

        set
        {
            if(value <= 0)
            {
                Debug.Log("no win is less than " + value);
                value = 0;
            }
            noWin = value;
        }
    }

    public int POUND500
    {
        get
        {
            return pound500;
        }

        set
        {
            if (value <= 0)
            {
                Debug.Log("500 pound prize is finished " + value);
                value = 0;
            }
            pound500 = value;
        }
    }
}

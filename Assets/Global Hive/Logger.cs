using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : Singleton<Logger>
{
    public void Log(string log)
    {
        Debug.Log(log);
    }

    public void LogE(string log)
    {
        Debug.LogError(log);
    }
}

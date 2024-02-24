using System;
using UnityEngine;

public class CoreGameSignals : MonoBehaviour
{
    public static CoreGameSignals Instance;

    private void Awake()
    {
        Instance = this;
    }
    public Action onStartGame = delegate { };
    public Action onRestartGame = delegate { };      
}

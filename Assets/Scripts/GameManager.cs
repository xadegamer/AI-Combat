using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] float battleDelay;

    private void Awake()
    {
        Instance =  this;
    }

    public float BattleDelay()
    {
        return battleDelay;
    }
}

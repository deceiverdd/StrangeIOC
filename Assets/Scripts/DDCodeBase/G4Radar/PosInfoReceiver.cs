using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BUDP;
using System.Net;

public class PosInfoReceiver : MonoSingleton<PosInfoReceiver>
{
    private string ip;
    private int port;
    private LightUDP posRecUDP;

    protected override void Awake()
    {
        base.Awake();
        InitData();
        InitUDP();
    }

    // Use this for initialization
    void Start()
    {
        
    }

    private void InitUDP()
    {
        posRecUDP = new LightUDP(IPAddress.Parse(this.ip), this.port);
        posRecUDP.DGramRecieved += ReceiveG4Data;
    }

    private void ReceiveG4Data(object sender, BUDPGram dgram)
    {
        List<G4DataReceived.MyPoint> posList = G4DataReceived.DataRec(Convert.ToBase64String(dgram.data));

        switch (GameStateMgr.CurGameState)
        {
            case GameStateMgr.GameState.Standby:
                break;
            case GameStateMgr.GameState.Loading:
                break;
            case GameStateMgr.GameState.Gaming:
                break;
            default:
                break;
        }
    }

    private void InitData()
    {
        this.ip = GameConfig.Instance.ServerIP;
        this.port = int.Parse(GameConfig.Instance.SeverPort);
    }

    private void OnDestroy()
    {
        Debuger.Log("destroy " + this.gameObject.name);
    }

    private void OnApplicationQuit()
    {
        posRecUDP.Close();
    }
}

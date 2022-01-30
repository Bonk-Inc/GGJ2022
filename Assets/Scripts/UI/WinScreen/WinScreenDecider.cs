using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class WinScreenDecider : MonoBehaviour
{
    private const string recentWinPlayerPref = "RecentWin";

    private const string demonWin = "DemonWin";
    private const string angelWin = "AngelWin";
    private const string dualityWin = "HumanWin";

    [SerializeField]
    private WinScreenData demonData, angelData, humanData;

    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private Image mcPortrait;



    private void Awake()
    {
        var win = PlayerPrefs.GetString(recentWinPlayerPref);

        switch (win)
        {
            case demonWin:
                SetUI(demonData);
                break;
            case angelWin:
                SetUI(angelData);
                break;
            default:
                SetUI(humanData);
                break;
        }
        
    }

    private void SetUI(WinScreenData data) {
        title.text = data.title;
        mcPortrait.sprite = data.image;
        data.extra.SetActive(true);
    }


    [Serializable]
    public struct WinScreenData
    {
        public string title;
        public Sprite image;
        public GameObject extra;
    }
}

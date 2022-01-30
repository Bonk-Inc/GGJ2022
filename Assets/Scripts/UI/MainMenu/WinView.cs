using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinView : MonoBehaviour
{
    [SerializeField]
    private string winKey;

    [SerializeField]
    private Image winSprite;
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private Color hiddenColor, shownColor;

    private void Awake()
    {
        var key = PlayerPrefs.GetInt(winKey);
        if(key == 1) {
            winSprite.color = shownColor;
            title.color = shownColor;
        }
        else {
            winSprite.color = hiddenColor;
            title.color = hiddenColor;
        }
    }
}

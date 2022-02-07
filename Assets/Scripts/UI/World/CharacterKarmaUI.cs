using UnityEngine;
using UnityEngine.UI;

public class CharacterKarmaUI : MonoBehaviour
{
    private int max;

    [SerializeField]
    private Image mask, fill;

    private void Start()
    {
        max = GameManager.instance.karma.MaxKarma;
        GameManager.instance.karma.OnKarmaChange += GetCurrentFill;

        UpdateFill(GameManager.instance.karma.CurrentKarma);
    }

    private void OnDestroy()
    {
        GameManager.instance.karma.OnKarmaChange -= GetCurrentFill;
    }

    private void GetCurrentFill(object caller, Karma.KarmaChangeArgs args)
    {
        UpdateFill(args.karma);
    }

    private void UpdateFill(int karma) {
        var fillAmount = (float) karma / (float)max;
        mask.fillAmount = fillAmount;
        
        fill.color = (fillAmount >= 0.6f) ? Color.blue : (fillAmount <= 0.4f) ? Color.red : Color.magenta;
    }
}
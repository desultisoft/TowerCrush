using UnityEngine;
using DG.Tweening;

public class SpriteTweener : MonoBehaviour
{
    private Sequence colorTween;
    private SpriteRenderer rend;

    public void Init(SpriteRenderer rend)
    {
        colorTween = DOTween.Sequence();
        colorTween.TogglePause();
        colorTween.Append(rend.DOColor(Color.red, 0.2f)).SetEase(Ease.Linear);
        colorTween.Append(rend.DOColor(rend.color, 0.2f)).SetEase(Ease.Linear);
        colorTween.SetAutoKill(false);
    }

    public void Tween()
    {
        colorTween.Restart();
    }
}

using UnityEngine;

public class TowerDetector : MonoBehaviour
{
    public Tower owner { private set; get; }
    private void Start()
    {
        owner = GetComponentInParent<Tower>();
    }

    public SpriteRenderer towerSprite;
    public SpriteRenderer turretSprite;
    public int _overlaps;

    public bool isOverlapping
    {
        get
        {
            return _overlaps > 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (enabled)
        {
            _overlaps++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (enabled)
        {
            _overlaps--;
        }
    }

    public void Update()
    {
        if (!isOverlapping)
        {
            ChangeNonRedColors(255, towerSprite);
            ChangeNonRedColors(255, turretSprite);
        }
        else
        {
            ChangeNonRedColors(0, towerSprite);
            ChangeNonRedColors(0, turretSprite);
        }
    }

    public void OnDisable()
    {
        //Debug.Log("Changing color back!");
        ChangeNonRedColors(255, towerSprite);
        ChangeNonRedColors(255, turretSprite);
    }

    void ChangeNonRedColors(float newColor, SpriteRenderer target)
    {
        if (!target) return;

        Color c = target.color;
        c.b = newColor;
        c.g = newColor;
        target.color = c;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public List<SpellButton> spellButtons;

    public UISlider slider;
    public Transform content;
    public SpellButton spellButtonPrefab;

    private SpellButton spellButton;

    public static Spells instance;

    public int index;

    public void Start()
    {
        instance = this;

        spellButtons = new List<SpellButton>();

        for (int i = 0; i < 5; i++)
        {
            spellButton = Instantiate(spellButtonPrefab);
            spellButton.transform.SetParent(content.transform);
            spellButtons.Add(spellButton);
        }
    }

    public void AddTower(SpellTower added)
    {
        spellButtons[index].SetTower(added);;

        slider.SlideIn();
        index++;
    }

    public void RemoveTower(SpellTower spellTower)
    {

        spellButtons[index].SetTower(null);
        index--;
        if (index <= 0)
        {
            slider.SlideOut();
        }
    }
}

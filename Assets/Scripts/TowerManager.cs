using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;
    public TowerButton buttonPrefab;
    public Transform towerContent;
    public RectTransform towerUIRect;
    public GameObject towerRange;
    public LayerMask towerLayer;
    public Tower selectedTower;

    public UISlider UpgradeSlider;
    public UISlider BuildableSlider;

    private bool isShowing;
    private Vector3 WorldPosition;
    private Tower spawnedTower;
    private TowerButton tempButton;
    private Dictionary<Tower, TowerButton> buttonDict;
    private RaycastHit hit;
    private List<Tower> upgradedTowers;
    private Tower selectedForBuild;
    private Tower mousedOverTower;
    private Vector2 buttonsPos;
    

    public void Start()
    {
        instance = this;

        buttonDict = new Dictionary<Tower, TowerButton>();
        upgradedTowers = new List<Tower>();

        CreateButtons();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (BuildableSlider.isSlidIn || UpgradeSlider.isSlidIn)
            {
                isShowing = false;
                BuildableSlider.SlideOut();
                UpgradeSlider.SlideOut();

                selectedTower = null;
                selectedForBuild = null;
                Destroy(spawnedTower);
            }
            else
            {
                isShowing = true;
                ShowBuildableTowers();
                BuildableSlider.SlideIn();
            }
        }

        if (spawnedTower)
        {
            if (!isShowing)
            {
                Destroy(spawnedTower.gameObject);
            }
            else
            {
                MoveSpawnedTower();
            }
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            towerRange.SetActive(false);
        }
        else
        {
            FindMousedOverTower();
            ShowMousedOverTowerRange();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (selectedForBuild)
                {
                    Build(spawnedTower);
                    return;
                }

                if (mousedOverTower)
                {
                    //Debug.Log("Showing options1!");
                    ShowTowerOptions(mousedOverTower);
                    return;
                }
            }
        }

        
    }

    private void ShowBuildableTowers()
    {
        foreach (KeyValuePair<Tower, TowerButton> buttonTowerPair in buttonDict)
        {
            TowerButton button = buttonTowerPair.Value;
            if (!upgradedTowers.Contains(buttonTowerPair.Key))
            {
                button.gameObject.SetActive(true);
            }
        }
    }

    public void CreateButtons()
    {
        GameObject[] towers = Resources.LoadAll<GameObject>("Towers");
        foreach (GameObject towerPrefab in towers)
        {
            Tower t = towerPrefab.GetComponent<Tower>();
            if (t)
            {
                tempButton = Instantiate(buttonPrefab);
                tempButton.Init(t);
                tempButton.gameObject.SetActive(true);
                buttonDict.Add(t, tempButton);
                upgradedTowers.AddRange(t.data.upgrades);
            }
        }

        //Add Basic Towers to buildableTowers instead.
        foreach (KeyValuePair<Tower, TowerButton> buttonTowerPair in buttonDict)
        {
            TowerButton button = buttonTowerPair.Value;
            if (!upgradedTowers.Contains(buttonTowerPair.Key))
            {
                button.transform.SetParent(BuildableSlider.content);
            }
            else
            {
                button.transform.SetParent(UpgradeSlider.content);
            }
        }
    }

    public void ShowTowerOptions(Tower aTower)
    {
        foreach(Tower tower in buttonDict.Keys)
        {
            TowerButton button = buttonDict[tower];

            if (aTower.data.upgrades.Contains(tower))
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }

        selectedTower = aTower;
        BuildableSlider.SlideOut();
        UpgradeSlider.SlideIn();
    }

    public void SelectForBuilding(Tower tower)
    {
        if(spawnedTower)
            Destroy(spawnedTower.gameObject);

        selectedForBuild = tower;
        spawnedTower = Instantiate(selectedForBuild);

        spawnedTower.enabled = false;
        spawnedTower.tDetector.enabled = true;

        spawnedTower.transform.position = WorldPosition;
    }

    bool CanAfford(Tower t)
    {
        return t.data.cost <= MoneyManager.instance.money;
    }

    private void MoveSpawnedTower()
    {
        //Move around a tower to spawn if we have one.
        if (spawnedTower)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            WorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            WorldPosition.x = Mathf.RoundToInt(WorldPosition.x);
            WorldPosition.y = Mathf.RoundToInt(WorldPosition.y);
            WorldPosition.z = 0;
            spawnedTower.transform.position = WorldPosition;
        }
    }

    private void Build(Tower toBuy)
    {
        if (toBuy && CanAfford(toBuy) && !toBuy.tDetector.isOverlapping)
        {
            MoneyManager.instance.SpendMoney(toBuy.data.cost);
            toBuy.enabled = true;
            toBuy.tDetector.enabled = false;
            toBuy.TotalValue += toBuy.data.cost;

            if(toBuy is SpellTower spellTower)
            {
                Spells.instance.AddTower(spellTower);
            }

            spawnedTower = Instantiate(toBuy);
            spawnedTower.enabled = false;
            spawnedTower.tDetector.enabled = true;
        }
    }

    public void Upgrade(Tower newTowerType)
    {
        if (CanAfford(newTowerType))
        {
            MoneyManager.instance.SpendMoney(newTowerType.data.cost);
            
            Tower newTower = Instantiate(newTowerType);
            newTower.transform.position = selectedTower.transform.position;

            newTower.TotalValue = newTowerType.data.cost + selectedTower.TotalValue;

            newTower.enabled = true;
            newTower.tDetector.enabled = true;

            Destroy(selectedTower.gameObject);
            selectedTower = null;

            UpgradeSlider.SlideOut();
        }
    }

    public void Sell()
    {
        if (selectedTower is SpellTower spellTower)
        {
            Spells.instance.RemoveTower(spellTower);
        }
        MoneyManager.instance.GainMoney(Mathf.CeilToInt(selectedTower.TotalValue * 0.75f));
        Destroy(selectedTower.gameObject);

        UpgradeSlider.SlideOut();
    }

    private void FindMousedOverTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, towerLayer);
        if (hit.collider != null)
        {
            TowerDetector towerDetector = hit.collider.GetComponent<TowerDetector>();
            if (towerDetector && towerDetector.owner)
            {
                mousedOverTower = towerDetector.owner;
            }
        }
        else
        {
            mousedOverTower = null;
        }
    }

    private void ShowMousedOverTowerRange()
    {
        if (mousedOverTower)
        {
            towerRange.SetActive(true);
            towerRange.transform.localScale = Vector3.one * mousedOverTower.range;
            towerRange.transform.position = mousedOverTower.transform.position;
        }
        else
        {
            towerRange.SetActive(false);
        }
    }
}

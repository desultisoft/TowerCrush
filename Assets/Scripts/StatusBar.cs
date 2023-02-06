using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public List<Status> allStatus;
    public Image buffIconPrefab;

    public void Start()
    {
        allStatus = new List<Status>();
    }

    public void AddStatus(Status s)
    {
        
    }

    public void HandleStatus(Status s, bool isActive)
    {
        if (isActive)
        {
            Image buff = Instantiate(buffIconPrefab);
            buff.transform.SetParent(transform);
            buff.sprite = s.image;
        }
    }
}
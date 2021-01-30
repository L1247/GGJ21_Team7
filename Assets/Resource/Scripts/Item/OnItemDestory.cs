using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnItemDestory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> activateObjects = new List<GameObject>();

    // Start is called before the first frame update
    private void OnDestroy()
    {
        foreach (var activateObject in activateObjects)
        {
           activateObject.SetActive(true);
        }
    }
}

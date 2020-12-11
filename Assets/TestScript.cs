using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    public GameObject Module;

    void Start()
    {
        foreach (Flash k in Module.GetComponentsInChildren<Flash>())
        {
            k.FlashForSeconds(60f);
        }
    }
}

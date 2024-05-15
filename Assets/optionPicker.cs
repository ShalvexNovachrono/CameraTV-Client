using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class optionPicker : MonoBehaviour
{
    [SerializeField] public GameObject one;
    [SerializeField] public GameObject two;


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            one.SetActive(true);
            transform.gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.B))
        {
            two.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserLight : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Light light;

    
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        light.transform.LookAt(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

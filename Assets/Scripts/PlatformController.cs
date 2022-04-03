using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public string color;
    private bool _isActive;

    // Start is called before the first frame update
    void Start()
    {
        _isActive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetActive(bool status)
    {
        _isActive = status;
        gameObject.SetActive(_isActive);
    }
}

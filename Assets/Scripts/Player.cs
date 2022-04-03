using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isDead;

    // Start is called before the first frame update
    void Start()
    {
        _isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsDead()
    {
        return _isDead;
    }

    public void SetDead(bool isDead)
    {
        _isDead = isDead;
    }
}

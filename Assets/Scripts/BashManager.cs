using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashManager : MonoBehaviour
{
    [SerializeField] float _slowdownFactor = 0.05f;

    float _timeFactor = 1f;
    bool _bashActive = false;
    GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getTimeFactor()
    {
        return _timeFactor;
    }

    public bool isBashActive()
    {
        return _bashActive;
    }

    public void startBash(GameObject target)
    {
        _bashActive = true;
        _target = target;
        _timeFactor = _slowdownFactor;
    }

    public void releaseBash()
    {
        _bashActive = false;
        _timeFactor = 1f;
    }
}

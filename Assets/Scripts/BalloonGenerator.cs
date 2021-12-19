using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject balloonObject;
    [SerializeField] private float generateTime = 2f;
    
    [HideInInspector] public GameObject currentBalloon = null;
    
     private bool _isGenerate = true; 
    
    //private static List<GameObject> _balloons = new List<GameObject>();

    void Update()
    {
        if(_isGenerate)
            StartCoroutine(AutoGenerate(balloonObject));
    }

    private IEnumerator AutoGenerate(GameObject balloon)
    {
        _isGenerate = false;
        GenerateBalloon(balloon);
        yield return new WaitForSeconds(generateTime);
        _isGenerate = true;
    }

    private void GenerateBalloon(GameObject balloon)
    {
        currentBalloon= Instantiate(balloon, transform.position, Quaternion.identity);
        currentBalloon.transform.parent = transform;
        //_balloons.Add(currentBalloon);
    }
}

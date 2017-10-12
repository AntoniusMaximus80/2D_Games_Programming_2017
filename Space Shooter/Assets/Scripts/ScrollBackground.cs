using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

    [SerializeField]
    private GameObject _firstBackground;
    
    [SerializeField]
    private GameObject _secondBackground;

    private float _distanceBetweenBackgrounds;

    // Use this for initialization
    void Start () {
        _distanceBetweenBackgrounds = _firstBackground.transform.position.y -
            _secondBackground.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        _firstBackground.transform.Translate(Vector3.down * Time.deltaTime * 4f);
        _secondBackground.transform.Translate(Vector3.down * Time.deltaTime * 4f);
        if (_firstBackground.transform.position.y <= -32f)
        {
            _firstBackground.transform.Translate(0f, _firstBackground.transform.position.y + _distanceBetweenBackgrounds, 0f);
        }
        if (_secondBackground.transform.position.y <= -32f)
        {
            _secondBackground.transform.Translate(0f, _secondBackground.transform.position.y + _distanceBetweenBackgrounds, 0f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

    [SerializeField]
    private GameObject _firstBackground;
    
    [SerializeField]
    private GameObject _secondBackground;

    private float _backgroundHeight;

    private Renderer _renderer;

    // Use this for initialization
    void Start () {
        _backgroundHeight = _firstBackground.GetComponent<Renderer>().bounds.size.y;
    }
	
	// Update is called once per frame
	void Update () {
        _firstBackground.transform.Translate(Vector3.down * Time.deltaTime * 4f);
        _secondBackground.transform.Translate(Vector3.down * Time.deltaTime * 4f);

        if (_firstBackground.transform.position.y <= -32f)
        {
            _firstBackground.transform.Translate(0f, _backgroundHeight * 2f, 0f);
        }

        if (_secondBackground.transform.position.y <= -32f)
        {
            _secondBackground.transform.Translate(0f, _backgroundHeight * 2f, 0f);
        }
    }
}

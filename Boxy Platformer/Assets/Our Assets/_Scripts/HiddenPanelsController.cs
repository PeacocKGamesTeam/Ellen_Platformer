using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPanelsController : MonoBehaviour {


    public List<GameObject> panelsToHide;



	// Use this for initialization
	void Start () {
		
        foreach(GameObject obj in panelsToHide)
        {
            obj.SetActive(false);
        }


	}
}

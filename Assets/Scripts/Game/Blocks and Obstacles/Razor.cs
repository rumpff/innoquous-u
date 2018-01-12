using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razor : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - (Time.deltaTime * 900));
	}
}

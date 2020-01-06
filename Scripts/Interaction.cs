using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
	public Transform _platform;
	bool _isUp = false
		;
	/*private void OnTriggerStay(Collider other)
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			
			StartCoroutine(Elevator());
			if (_isUp == false)
			{
				_isUp = true;
			}
			else
			{
				_isUp = false;
			}
		}
	}*/

	public IEnumerator Elevator()
	{
		for (float ft = 10f; ft >= 0; ft -= 0.1f)
		{
			if (_isUp == false)
			{
				_platform.Translate(new Vector3(0, ft * Time.deltaTime));
				
			}
			else
			{
				_platform.Translate(new Vector3(0, -ft * Time.deltaTime));
				
			}
			
		}
		_isUp = !_isUp;
		yield return null;
	}
}

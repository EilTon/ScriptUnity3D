using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraControler : MonoBehaviour
{
	#region Declarations public
	public Transform _target;
	public Vector3 _offset;
	public float _rotateSpeed;
	public Transform _pivot;
	public float _maxViewAngle;
	public float _minViewAngle;
	public bool _invertY;
	public bool _switchCameraToFirstPerson;
	#endregion

	#region Declarations private
	float _xRotation = 0f;
	#endregion

	#region Declarations Event Args

	#endregion

	#region Declarations Event Handler

	#endregion

	#region Declarations Event Call

	#endregion

	#region Functions Unity
	private void Awake()
	{
		#region Initialize

		#endregion
	}

	private void Start()
	{
		#region Initialize
		if (_switchCameraToFirstPerson == false)
		{
			_offset = _target.position - transform.position;
			_pivot.transform.position = _pivot.transform.position;
			_pivot.transform.parent = _target.transform;
		}
		else
		{
			transform.parent = _target.transform;
			transform.position = _target.position;
		}

		Cursor.lockState = CursorLockMode.Locked;
		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		#endregion

		#region Timer

		#endregion
	}

	private void FixedUpdate()
	{
		#region Movement

		#endregion

		#region Actions

		#endregion

		#region Timer

		#endregion
	}

	private void LateUpdate()
	{
		float horizontal = Input.GetAxis("Mouse X") * _rotateSpeed * Time.deltaTime;
		float vertical = Input.GetAxis("Mouse Y") * _rotateSpeed * Time.deltaTime;
		if (_switchCameraToFirstPerson == false)
		{
			FollowCameraThirdPerson(horizontal, vertical);
		}
		else
		{
			FollowCameraFirstPerson(horizontal, vertical);
		}

	}
	#endregion

	#region Helper
	void FollowCameraThirdPerson(float horizontal, float vertical)
	{

		_target.Rotate(0, horizontal, 0);

		//_target.Rotate(-vertical, 0, 0);
		if (_invertY)
		{
			_pivot.Rotate(vertical, 0, 0);
		}
		else
		{
			_pivot.Rotate(-vertical, 0, 0);
		}

		_xRotation -= vertical;
		_xRotation = Mathf.Clamp(_xRotation, _minViewAngle, _maxViewAngle);
		_pivot.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

		/*//Limit Up/Down camera rotation
		if (_pivot.rotation.eulerAngles.x > _maxViewAngle && _pivot.rotation.eulerAngles.x < 180f)
		{
			_pivot.rotation = Quaternion.Euler(_maxViewAngle, 0, 0);
		}
		if (_pivot.rotation.eulerAngles.x > 180 && _pivot.rotation.eulerAngles.x < 360f + _minViewAngle)
		{
			_pivot.rotation = Quaternion.Euler(360f + _minViewAngle, 0, 0);
		}*/

		//Move the Camera base on the current rotation
		float desiredYAngle = _pivot.eulerAngles.y;
		float desiredXAngle = _pivot.eulerAngles.x;
		Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
		transform.position = _target.position - (rotation * _offset);

		if (transform.position.y < _target.position.y)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
		}
		transform.LookAt(_target);

	}

	void FollowCameraFirstPerson(float horizontal, float vertical)
	{
		_xRotation -= vertical;
		_xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
		_target.Rotate(0, horizontal, 0);
	}
	#endregion
}

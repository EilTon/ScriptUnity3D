using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
	#region Declarations public
	public float _speed;
	public float _jumpForce;
	public float _gravityScale;
	public float _rotateSpeed;
	public Transform _pivot;
	#endregion

	#region Declarations private
	private CharacterController _characterController;
	private Vector3 movement;
	private Camera _camera;
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
		_characterController = GetComponent<CharacterController>();
		_camera = Camera.main;
		#endregion
	}

	private void Update()
	{
		#region Movement
		MoveCharacter();
		#endregion

		#region Actions
		Debug.DrawRay(_camera.transform.position, _camera.transform.forward);
		Interaction();
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
	#endregion

	#region Helper
	void MoveCharacter()
	{
		float yStore = movement.y;
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");
		//movement = new Vector3(moveHorizontal, movement.y, moveVertical);
		movement = (transform.forward * moveVertical) + (transform.right * moveHorizontal);
		movement = movement.normalized * _speed;
		movement.y = yStore;
		if (_characterController.isGrounded)
		{
			movement.y = 0f;
			if (Input.GetButtonDown("Jump"))
			{
				movement.y = _jumpForce;
			}
		}
		movement.y = movement.y + (Physics.gravity.y * _gravityScale * Time.deltaTime);
		_characterController.Move(movement * Time.deltaTime);

		//move the player base on camera look
		if (moveHorizontal != 0 || moveVertical != 0)
		{
			Quaternion newRotation;
			if(Input.GetKey(KeyCode.S))
			{
				newRotation = Quaternion.LookRotation(new Vector3(movement.x, 0f, 0f));
			}
			else
			{
				newRotation = Quaternion.LookRotation(new Vector3(movement.x, 0f, movement.z));
			}
			
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _rotateSpeed * Time.deltaTime);
		}
	}
	void Interaction()
	{
		if(Input.GetMouseButtonDown(1))
		{
			//Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(_camera.transform.position,_camera.transform.forward,out hit))
			{
				hit.collider.GetComponent<Interaction>().Elevator();
			}
		}
	}
	#endregion
}

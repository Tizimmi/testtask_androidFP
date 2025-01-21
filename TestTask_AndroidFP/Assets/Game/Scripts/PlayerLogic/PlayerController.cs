using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.PlayerLogic
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private InputActionReference _moveAction;
		[SerializeField]
		private InputActionReference _lookAction;
		[SerializeField]
		private float _moveSpeed;
		[SerializeField]
		private float _lookSpeed;

		private CharacterController _controller;
		private float _currentXRotation;
		private Vector3 _velocity;
		
		private const float Gravity = -9.81f;

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
		}

		private void Update()
		{
			PlayerMove();
			PlayerLook();
			ApplyGravity();
		}

		private void PlayerMove()
		{
			var input = _moveAction.action.ReadValue<Vector2>();

			var moveZ = input.y;
			var moveX = input.x;

			var moveDirection = transform.forward * moveZ + transform.right * moveX;
			moveDirection.y = 0;

			if (moveDirection.magnitude > 1)
				moveDirection.Normalize();

			_controller.Move(moveDirection * (_moveSpeed * Time.deltaTime));
		}

		private void ApplyGravity()
		{
			_velocity.y += Gravity * Time.deltaTime;
			_controller.Move(_velocity * Time.deltaTime);
		}

		private void PlayerLook()
		{
			var lookInput = _lookAction.action.ReadValue<Vector2>();

			var horizontalLook = lookInput.x * _lookSpeed;
			var verticalLook = -lookInput.y * _lookSpeed;

			transform.Rotate(0, horizontalLook, 0);
			_currentXRotation += verticalLook;

			_currentXRotation = Mathf.Clamp(_currentXRotation, -60f, 60f);

			var xQuaternion = Quaternion.Euler(_currentXRotation, transform.eulerAngles.y, 0);

			transform.rotation = xQuaternion;
		}
	}
}
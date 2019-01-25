using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerMove : MonoBehaviour, IPlayerMoveActions
{
    [Header("Controls")]
    [SerializeField]
    private InputMaster controls;

    [Header("Attributes")]
    [SerializeField]
    [Tooltip("Movement speed of the Player")]
    [Range(0.0f, 5f)]
    private float speed;

    private Vector2 direction;
    private Vector2 move;

    private void Awake()
    {
        controls.PlayerMove.SetCallbacks(this);       
    }

    private void OnEnable()
    {
        controls.PlayerMove.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerMove.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        move = new Vector2(speed / 100, speed / 100);
    }

    private void Update()
    {
        if (direction != Vector2.zero)
        {
            transform.Translate(move * direction);
        }
    }

}

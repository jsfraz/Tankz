using Unity.Netcode;
using UnityEngine;

public class ServerPlayerMovement : NetworkBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private PlayerInput input;

    void Start()
    {
        input = new PlayerInput();
        input.Enable();
    }

    private void Update()
    {
        // read movement
        Vector2 movement = input.Player.Movement.ReadValue<Vector2>();
        // determine player or server
        if (IsServer && IsLocalPlayer)
        {
            // move if server
            Move(movement);
        }
        else if (IsClient && IsLocalPlayer)
        {
            // move if player
            MoveServerRpc(movement);
        }
    }

    // move player
    private void Move(Vector2 movement)
    {
        Vector3 calculatedMove = movement.x * transform.right + movement.y * transform.forward;
        controller.Move(calculatedMove * speed * Time.deltaTime);
    }

    // request player to move
    [ServerRpc]
    private void MoveServerRpc(Vector2 movement)
    {
        Move(movement);
    }
}
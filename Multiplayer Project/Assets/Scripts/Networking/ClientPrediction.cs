using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ClientPrediction : NetworkBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CharacterController cc;
    
    public struct PlayerInput
    {
        public uint timestamp;
        public Vector2 input;
        public bool isJumping;
    }

    public struct GameState
    {
        public PlayerInput usedInput;
        public Vector3 position;
    }

    private List<PlayerInput> moveMemory;
    
    private PlayerInput lastInput;
    private GameState currentPredictedState;

    [SyncVar(hook = nameof(OnStateReceive))]
    public GameState serverState;

    private void Awake()
    {
        PlayerInput input = new PlayerInput
        {
            timestamp = 0,
            input = Vector2.zero,
        };

        lastInput = input;
    }

    private void Start()
    {
        if (isLocalPlayer)
            moveMemory = new List<PlayerInput>();
    }

    private void FixedUpdate() // TODO potentially take input in the update and movement in the fixedUpdate
    {
        if (isLocalPlayer)
        {
            // Get input from the client
            PlayerInput currentInput = new PlayerInput
            {
                timestamp = lastInput.timestamp + 1,
                input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
                isJumping = Input.GetButton("Jump"),
            };

            if (currentInput.input != Vector2.zero)
            {
                lastInput = currentInput;
                // Send input to server
                CmdSendInputToServer(currentInput);
                // Store the input in the list
                moveMemory.Add(currentInput);
                // Localy calculate movement
                UpdateCurrentPredictedState();
            }
        }
        
        SyncState();
        // Syncing between client and server state
        //SyncState();
        
    }

    // Call calculateMovement ONLY from server.
    [Command]
    private void CmdSendInputToServer(PlayerInput playerInput)
    {
        serverState = playerMovement.CalculateMovement(serverState, playerInput);
        Debug.Log($"calculated new state for player, syncing in progress... stateTimestamp={serverState.usedInput.timestamp}, statePos={serverState.position}");
    }

    private void OnStateReceive(GameState oldState, GameState newState)
    {
        serverState = newState;
        Debug.Log($"Got new state from server! (stateTimestamp={serverState.usedInput.timestamp}, statePos={serverState.position})");
        if (moveMemory != null)
        {
            while (moveMemory.Count > currentPredictedState.usedInput.timestamp - serverState.usedInput.timestamp) {
                moveMemory.RemoveAt(0);
            }
        
            UpdateCurrentPredictedState();
        }
    }

    private void UpdateCurrentPredictedState()
    {
        currentPredictedState = serverState;
        
        foreach (PlayerInput input in moveMemory)
        {
            currentPredictedState = playerMovement.CalculateMovement(currentPredictedState, input);
            Debug.Log($"Calculating predictedState based on server state, predictedState -> {currentPredictedState.position} (used input: {input.input},{input.timestamp})");
        }
    }

    private void SyncState()
    {
        if (isServer)
        {
            transform.position = serverState.position;
            return;
        }

        //  if (!isLocalPlayer) return;
      
        transform.position = Vector3.Lerp(transform.position, currentPredictedState.position, 0.2f);
        //Debug.Log($"Syncing states, current state: {transform.position}, predicted state: {currentPredictedState.position}");
    }
}

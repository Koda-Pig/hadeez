using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private GameInput gameInput;

    [SerializeField]
    private LayerMask countersLayerMask;

    [SerializeField]
    private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractionDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one player instance. That ain't right.");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInterAction;
        gameInput.OnInteractAlternateAction += GameInput_OnAlternateInteraction;
    }

    private void GameInput_OnAlternateInteraction(object sender, EventArgs e)
    {
        selectedCounter?.InteractAlternate(this);
    }

    private void GameInput_OnInterAction(object sender, System.EventArgs e)
    {
        selectedCounter?.Interact(this);
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() => isWalking;

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);

        float playerRadius = .7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDirection,
            moveDistance
        );

        if (!canMove)
        {
            // Cannot move towards this moveDirection
            // Attempt only X movement
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            bool isTryingToMoveX = moveDirection.x != 0;

            canMove =
                isTryingToMoveX
                && !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    moveDirectionX,
                    moveDistance
                );

            if (canMove)
            {
                // Can move on x-axis
                moveDirection = moveDirectionX;
            }
            else
            {
                // cannot move only on the X axis, try Z
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                bool isTryingToMoveZ = moveDirection.z != 0;

                canMove =
                    isTryingToMoveZ
                    && !Physics.CapsuleCast(
                        transform.position,
                        transform.position + Vector3.up * playerHeight,
                        playerRadius,
                        moveDirectionZ,
                        moveDistance
                    );

                if (canMove)
                {
                    // can move only on the Z axis
                    moveDirection = moveDirectionZ;
                }
                else
                {
                    // can't move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }
        isWalking = moveDirection != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(
            transform.forward,
            moveDirection,
            Time.deltaTime * rotateSpeed
        );
    }

    private void HandleInteractions()
    {
        float interactDistance = 2f;
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractionDirection = moveDirection;
        }

        if (
            Physics.Raycast(
                transform.position,
                lastInteractionDirection,
                out RaycastHit raycastHit,
                interactDistance,
                countersLayerMask
            )
        )
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter == selectedCounter)
                    return;
                selectedCounter = baseCounter;
                SetSelectedCounter(baseCounter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(
            this,
            new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter } // This is strange looking.
        );
    }

    public bool HasKitchenObject() => kitchenObject != null;

    public Transform GetKitchenObjectFollowTransform() => kitchenObjectHoldPoint;

    public KitchenObject GetKitchenObject() => kitchenObject;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject == null)
        {
            return;
        }
        OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}

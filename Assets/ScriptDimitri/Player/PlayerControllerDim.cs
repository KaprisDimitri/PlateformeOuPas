using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerDim : MonoBehaviour
{
    [SerializeField] PlayerInputDim playerInput;
    [SerializeField] PlayerMovementDim playerMovement;

    [SerializeField] Rigidbody rigidBody;
    [SerializeField] float vitesse;
    [SerializeField] float vitesseMax;
    [SerializeField] float vitesseJump;
    [SerializeField] float perteVitesse;

    [SerializeField] KeyCode jump;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;

    [SerializeField] Transform firstPositionForRayCast;
    [SerializeField] Transform SecondPositionForRayCast;
    [SerializeField] Transform rightSide;
    [SerializeField] Transform leftSide;
    [SerializeField] LayerMask layerGround;

    [SerializeField] float timeToReachWallJump;
    [SerializeField] float forceJumpWallY;
    [SerializeField] float forceJumpWallX;

    public Vector2 directionPlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerInput.InitPlayerInputDim(jump, left, right);
        playerMovement.InitPlayerMovement(firstPositionForRayCast, SecondPositionForRayCast, layerGround, rightSide,leftSide, timeToReachWallJump, forceJumpWallY, forceJumpWallX);
    }

    // Update is called once per frame
    void Update()
    {
        directionPlayer = playerInput.DirectionPlayer();
        playerMovement.MovePlayer(rigidBody, directionPlayer, vitesseMax, vitesse, vitesseJump, perteVitesse);
    }
}

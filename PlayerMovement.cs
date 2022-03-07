using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector3 v3Forcex;
    [SerializeField] Vector3 v3Forcey;
    [SerializeField] Vector3 v3Forcez;
    [SerializeField] Vector3 v3ForceMax;
    [SerializeField] Vector3 v3BoostForce;
    [SerializeField] Vector3 v3BounceForce;
    [SerializeField] private LayerMask layerCollide;
    [SerializeField] private Transform transformCheckGrounded;
    private Rigidbody playerBody;
    private bool boolRight;
    private bool boolLeft;
    private bool boolUp;
    private bool boolDown;
    private bool boolSpace;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            boolSpace = true;
        if (MainMenu.boolArrowsMove)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                boolRight = true;
            if (Input.GetKey(KeyCode.LeftArrow))
                boolLeft = true;
            if (Input.GetKey(KeyCode.UpArrow))
                boolUp = true;
            if (Input.GetKey(KeyCode.DownArrow))
                boolDown = true;
            if (Input.GetKeyUp(KeyCode.RightArrow))
                boolRight = false;
            if (Input.GetKeyUp(KeyCode.LeftArrow))
                boolLeft = false;
            if (Input.GetKeyUp(KeyCode.UpArrow))
                boolUp = false;
            if (Input.GetKeyUp(KeyCode.DownArrow))
                boolDown = false;
        }
        if (!MainMenu.boolArrowsMove)
        {
            if (Input.GetKey(KeyCode.D))
                boolRight = true;
            if (Input.GetKey(KeyCode.A))
                boolLeft = true;
            if (Input.GetKey(KeyCode.W))
                boolUp = true;
            if (Input.GetKey(KeyCode.S))
                boolDown = true;
            if (Input.GetKeyUp(KeyCode.D))
                boolRight = false;
            if (Input.GetKeyUp(KeyCode.A))
                boolLeft = false;
            if (Input.GetKeyUp(KeyCode.W))
                boolUp = false;
            if (Input.GetKeyUp(KeyCode.S))
                boolDown = false;
        }
    }
    // Update is called once per physics update
    private void FixedUpdate()
    {
        if (BoostonTouch.boolBoosted)
            playerBody.velocity = v3BoostForce;
        BoostonTouch.boolBoosted = false;

        if (BounceOnTouch.boolBounced)
            playerBody.velocity += v3BounceForce;
        BounceOnTouch.boolBounced = false;

        if (boolSpace && Physics.OverlapSphere(transformCheckGrounded.position, 0.3f, layerCollide).Length != 0)
            GetComponent<Rigidbody>().AddForce(v3Forcey, ForceMode.VelocityChange);
        if (boolRight && playerBody.velocity.x < v3ForceMax.x)
            playerBody.velocity += v3Forcex;
        if (boolLeft && playerBody.velocity.x > -v3ForceMax.x)
            playerBody.velocity -= v3Forcex;
        if (boolUp && playerBody.velocity.z < v3ForceMax.z)
            playerBody.velocity += v3Forcez;
        if (boolDown && playerBody.velocity.z > -v3ForceMax.z)
            playerBody.velocity -= v3Forcez;
        
        boolSpace = false;
    }
}

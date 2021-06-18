using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private void Update()
    {
        //Varibles that hold the input, using RAW to get just the raw input, no smoothing
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Varible that holds the direction, normalized makes diagonal movement possible(maybe)
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Checking if we are moving in any direction, magnitude is length
        if (direction.magnitude >= 0.1f)
        {
            //sets the dirrection of where we want our player to face
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            //Setting the smoothness of the rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            //Setting the roation of the player, rotates around the Y axis, hence why it was added at the Y position
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Creating a vector to determine that the player moves in the direction of the cam
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //add movement, deltaTime to make it frame rate independent
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
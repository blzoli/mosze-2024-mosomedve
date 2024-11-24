using UnityEngine;

/**
 * @class PlayerController
 * @brief Controls player movement within camera boundaries.
 */
public class PlayerController : MonoBehaviour
{
    /// @brief The movement speed of the player.
    public float moveSpeed = 5f;
    /// @brief The weapon equipped by the player.  
    public Weapon weapon;
    /// @brief The health of the player
    public static int health = 3;


    private Vector2 screenBounds; ///< The screen boundaries in world units.
    private float playerWidth;    ///< The half-width of the player sprite.
    private float playerHeight;   ///< The half-height of the player sprite.

    /**
     * @brief Initializes the player size and screen boundaries.
     */
    void Start()
    {
        // Calculate player width and height based on its SpriteRenderer bounds
        playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        playerHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        // Calculate screen bounds using the main camera
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    /**
     * @brief Updates the player position every frame.
     */
    void Update()
    {
        MovePlayer();
        ClampPosition();
        Shoot();
    }

    /**
     * @brief Moves the player based on WASD key inputs.
     *
     * If both opposing directions (e.g., W and S or A and D) are pressed, it cancels out the movement in that axis.
     */
    void MovePlayer()
    {
        float moveX = 0f;
        float moveY = 0f;

        // Check for individual key presses
        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // If both up and down, or both left and right are pressed, set the respective axis to zero
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) moveY = 0f;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) moveX = 0f;

        // Create a movement vector and normalize it if necessary
        Vector3 movement = new Vector3(moveX, moveY, 0);
        if (movement.magnitude > 1)
            movement.Normalize();

        // Apply the movement
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    /**
     * @brief Clamps the player's position within the screen boundaries.
     */
    void ClampPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x + playerWidth, screenBounds.x - playerWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y + playerHeight, screenBounds.y - playerHeight);
        transform.position = clampedPosition;
    }

    void Shoot()
    {
        void FireWeapon()
        {
            // Check if the player has a weapon equipped
            if (weapon != null)
            {
                // Fire the weapon
                weapon.Fire(transform.gameObject);
            }
            else Debug.Log("No weapon equipped!");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireWeapon();
        }
    }

    public void PickUpWeapon(Weapon weapon)
    {
        // Equip the new weapon
        this.weapon = weapon;
    }

    public void TakeDamage(int damage)
    {
        // Reduce health by the damage amount
        health -= damage;

        // Check if the player has run out of health
        if (health <= 0)
        {
            Game.GameOver();
            // Game over
            Debug.Log("Game Over!");
        }
    }

    public static void ResetPlayer()
    {
        // Reset the player's health
        health = 3;
    }


}

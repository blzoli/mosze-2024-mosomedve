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
    /// @brief The health of the player.
    public static int health = 5;

    private Vector2 screenBounds; ///< The screen boundaries in world units.
    private float playerWidth;    ///< The half-width of the player sprite.
    private float playerHeight;   ///< The half-height of the player sprite.

    private float fireRate = 0.25f; ///< Time between shots (4 shots per second)
    private float nextFireTime = 0f; ///< Time when the player can fire again

    private static GameObject explosion; ///< The explosion effect to play when the player is destroyed

    private GameObject mainSprite;
    private GameObject leftSprite;
    private GameObject rightSprite;


    /**
     * @brief Initializes the player size and screen boundaries.
     */
    void Start()
    {
        // Calculate player width and height based on its SpriteRenderer bounds
        playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        playerHeight = GetComponent<SpriteRenderer>().bounds.extents.y;


        GameObject player = this.transform.gameObject; 
        explosion = player.transform.Find("explosion").gameObject;
        mainSprite = player.transform.Find("ship").gameObject;
        leftSprite = player.transform.Find("shipleft").gameObject;
        rightSprite = player.transform.Find("shipright").gameObject;


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

        // switch sprites to face the direction of movement


        if (movement.x > 0)
        {
            mainSprite.SetActive(false);
            leftSprite.SetActive(false);
            rightSprite.SetActive(true);
        }
        else if (movement.x < 0)
        {
            mainSprite.SetActive(false);
            leftSprite.SetActive(true);
            rightSprite.SetActive(false);
        }
        else
        {
            mainSprite.SetActive(true);
            leftSprite.SetActive(false);
            rightSprite.SetActive(false);
        }

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

    /**
     * @brief Fires the player's weapon when the space key is pressed.
     */

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

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            FireWeapon();
            nextFireTime = Time.time + fireRate; // Set the next fire time
        }
    }

    /**
     * @brief Picks up a new weapon and equips it.
     * @param weapon The weapon to pick up.
     */
    public void PickUpWeapon(Weapon weapon)
    {
        // Equip the new weapon
        this.weapon = weapon;
    }

    /**
     * @brief Damages the player by a specified amount.
     * @param damage The amount of damage to inflict.
     */
    public void TakeDamage(int damage)
    {
        // Reduce health by the damage amount
        health -= damage;

        // Check if the player has run out of health
        if (health <= 0)
        {
            explosion.SetActive(true);
            Game.GameOver();
            // Game over
            Debug.Log("Game Over!");
        } else
        {
            // Play the damage sound
            if (Application.isPlaying) AudioManager.Instance.PlaySound("playerDamage");
        }
    }

    /**
     * @brief Resets the player's health to the default value.
     */
    public static void ResetPlayer()
    {
        explosion.SetActive(false);
        // Reset the player's health
        health = 5;
    }

    /**
     * @brief Increases the player's health by a specified amount.
     * @param healthBoost The amount of health to add.
     */
    public void IncreaseHealth(int healthBoost)
    {
        // Increase the player's health
        health += healthBoost;
    }
}

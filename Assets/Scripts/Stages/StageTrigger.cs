using UnityEngine;

/// @class StageTrigger
/// @brief This script triggers the next stage in the game upon collision. TESTING ONLY!
///
/// This MonoBehaviour checks for collisions and, when colliding with another GameObject,
/// it finds the Game instance and calls StartNextStage to proceed to the next stage.
public class StageTrigger : MonoBehaviour
{
    /// @brief Unity's OnTriggerEnter2D method.
    ///
    /// This method is called when this GameObject collides with another collider.
    /// It attempts to call StartNextStage on the Game instance.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Find the Game instance in the scene
        Game gameManager = FindObjectOfType<Game>();

        if (gameManager != null)
        {
            // Call StartNextStage on the Game instance
            if (collision.transform.tag == "Player")
            { 
                gameManager.StartNextStage();
                Debug.Log("Next stage triggered.");
            }
        }
        else
        {
            Debug.LogError("Game instance not found in the scene.");
        }
    }
}

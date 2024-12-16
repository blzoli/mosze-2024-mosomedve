using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Tests the game over condition when all stages are completed.
/// </summary>
public class GameCompleteCondition
{
    // A Test behaves as an ordinary method
    [Test]
    public void GameCompleteConditionSimplePasses()
    {
        Game.isOver = false;
        GameObject gameObj = new GameObject();
        Game game = gameObj.AddComponent<Game>();
        // Add asteroid and enemy prefabs
        GameObject asteroidPrefab = new GameObject("Asteroid");
        GameObject enemyPrefab = new GameObject("Enemy");
        game.asteroidPrefab = asteroidPrefab;
        game.enemyPrefab = enemyPrefab;
        ScoreLoader scoreloader = new ScoreLoader();


        Assert.IsTrue(!Game.isOver);

        gameObj.GetComponent<Game>().StartNextStage();
        Assert.IsTrue(!Game.isOver);

        int l = game.stages.Length;
        
        for (int i = 1; i < l; i++)  
        {  
            gameObj.GetComponent<Game>().StartNextStage();    
            Assert.IsTrue(!Game.isOver);    
        }

        gameObj.GetComponent<Game>().StartNextStage();  // This should trigger the game over condition, since no more stages are available

        Debug.Log("Game Over: " + Game.isOver);
        Debug.Log("stages length: " + game.stages.Length);
        Debug.Log("current stage: " + game.CurrentStageID);

        Assert.IsTrue(Game.isOver);
         
        Game.ResetGameState();

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator GameCompleteConditionWithEnumeratorPasses()
    {
        yield return null;
    }
}

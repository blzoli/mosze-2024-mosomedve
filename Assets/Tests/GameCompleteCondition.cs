using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameCompleteCondition
{
    // A Test behaves as an ordinary method
    [Test]
    public void GameCompleteConditionSimplePasses()
    {
        Game.isOver = false;
        GameObject gameObj = new GameObject();
        Game game = gameObj.AddComponent<Game>();

        Assert.IsTrue(!Game.isOver);

        gameObj.GetComponent<Game>().StartNextStage();
        Assert.IsTrue(!Game.isOver);

        int l = game.stages.Length;
        
        for (int i = 1; i < l-1; i++)  
        {  
            gameObj.GetComponent<Game>().StartNextStage();    
            Assert.IsTrue(!Game.isOver);    
        }

        gameObj.GetComponent<Game>().StartNextStage();  
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

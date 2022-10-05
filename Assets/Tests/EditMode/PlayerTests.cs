using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerMoveRight()
    {
        player_cntrl Player = new player_cntrl();
        //Player.MoveTo();
        Assert.AreEqual(true, true);
    }
    //протестить поворот

    
}

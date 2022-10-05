using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WoodTests
{
    GameObject Player;
    GameObject smallTree;
    GameObject inventory;

    [SetUp]
    public void SetUp()
    {
        Player = GameObject.Instantiate(new GameObject());
        Vector3 treePos = Player.transform.position;
        treePos.x += 0.5f;
        smallTree = MonoBehaviour.Instantiate(Resources.Load<GameObject>("small_tree"), treePos, Quaternion.identity);

        Player.AddComponent<player_cntrl>();
        Player.AddComponent<pocket_inventory>();
        Player.AddComponent<BoxCollider2D>();
        Player.AddComponent<Rigidbody2D>();
        Player.GetComponent<Rigidbody2D>().freezeRotation = true;
        Player.GetComponent<Rigidbody2D>().gravityScale = 0;

        //Player.GetComponent<pocket_inventory>().Init();
        Player.tag = "Player";
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.Destroy(smallTree);
        GameObject.Destroy(Player);

    }

    [Test]
    public void TreeTag()
    {
        Assert.AreEqual(smallTree.tag, "tree");
    }

    [Test]
    //if tree in front
    public void TreeRecognize()
    {
        Assert.AreEqual(Player.GetComponent<player_cntrl>().WhatIsInFrontOfPlayer(), "tree");
    }



    //tree is behind the player
    [Test]
    public void TreeNotRecognize()
    {
        Player.transform.Translate(Vector3.right);
        Assert.AreNotEqual(Player.GetComponent<player_cntrl>().WhatIsInFrontOfPlayer(), "tree");
    }

    //не попадает в триггер
    [UnityTest]
    public IEnumerator OneSmallTreeChopping()
    {
        Player.GetComponent<player_cntrl>().WhatIsInFrontOfPlayer();
        Player.GetComponent<player_cntrl>().ChopChop();
        Player.transform.Translate(Vector3.right * 0.5f);
        yield return new WaitForSeconds(2f);
        Assert.Greater(Player.GetComponent<pocket_inventory>().GetAmounts(0), 1);
    }
}
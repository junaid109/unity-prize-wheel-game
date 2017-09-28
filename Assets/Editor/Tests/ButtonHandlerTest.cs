using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class ButtonHandlerTest {

    private GameController _gc;
    private ButtonData _bd;
    private ButtonHandler _bh;

    [SetUp]
    public void SetUp()
    {
        _bh = new ButtonHandler();
        _bd = new ButtonData();
        _gc = new GameController();
    }

    [Test]
    public void HandleClickTest()
    {
        _bh = new ButtonHandler();
        _bd = new ButtonData();
        _gc = new GameController();

        _bh.HandleClick();
        _gc.TurnWheel();
        _gc.CheckForPrize(ButtonData.clickCount);

        Assert.AreNotEqual(1, ButtonData.clickCount);
    }

}
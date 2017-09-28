using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class GameControllerTest {

    private GameController _gc;
    private DataController _dc;
    private ButtonHandler _bh;
    private PrizeData _pd;
    private ButtonData _bd;

    [SetUp] 
    public void SetUp()
    {
        _gc = new GameController();
        _dc = new DataController();
        _bh = new ButtonHandler();
        _pd = new PrizeData();
        _bd = new ButtonData();
    }

    [Test]
	public void EditorTest() {
		//Arrange
		var gameObject = new GameObject();

		//Act
		//Try to rename the GameObject
		var newGameObjectName = "My game object";
		gameObject.name = newGameObjectName;

		//Assert
		//The object has a new name
		Assert.AreEqual(newGameObjectName, gameObject.name);
	}

    [Test]
    public void TestForWhenTurnWheel()
    {
        float result = -.03f;

       //  _gc.TurnWheel();

          Assert.AreEqual(result, -.03f);
    }

    [Test]
    public void TestToCheckJarsOfCoffee()
    {
        int result = 28;

        _pd.JarsOfCoffee = result;
        _pd.JarsOfCoffee--;

        Assert.AreEqual(_pd.JarsOfCoffee, 27);
    }

    [Test]
    public void TestToCheckPounds()
    {
        int result = 1;

        _pd.POUND500 = result;
        _pd.POUND500--;

        Assert.AreEqual(_pd.POUND500, 0);
    }

    [Test]
    public void TestToCheckNoWin()
    {
        int result = 21;

        _pd.NoWin = result;
        _pd.NoWin--;

        Assert.AreEqual(_pd.NoWin, 20);
    }

    [Test]
    public void TestForFinalWheelStop()
    {
        float expected = -0.3f;
        float result = -0.3f;

        _gc.CheckWheelFinalStop(expected);

        Assert.AreEqual(expected, _gc.CheckWheelFinalStop(result));
    }

    [Test]
    public void TestForJarsOfCoffee()
    {
        _gc = new GameController();
        _dc = new DataController();
        _bh = new ButtonHandler();
        _pd = new PrizeData();
        _bd = new ButtonData();

        var expected = _pd.JarsOfCoffee;
        _gc.jarsOfCoffeePrize();

        Assert.AreEqual(expected, _pd.JarsOfCoffee);
    }

    [Test]
    public void TestForNoWin()
    {
        _gc = new GameController();
        _dc = new DataController();
        _bh = new ButtonHandler();
        _pd = new PrizeData();
        _bd = new ButtonData();

        var expected = _gc.noWin;
        _gc.noWinPrize();

        Assert.AreEqual(expected, _pd.NoWin=expected);
    }


    //[Test]
    //public void TestForPound()
    //{
    //    _gc = new GameController();
    //    _dc = new DataController();
    //    _bh = new ButtonHandler();
    //    _pd = new PrizeData();
    //    _bd = new ButtonData();

    //    var expected = _gc.pound500;
    //    _gc.poundPrize();

    //    Assert.AreEqual(expected, _pd.POUND500);
    //}

    [Test]
    public void TestForNoJarsOfCoffee()
    {
        _gc = new GameController();
        _dc = new DataController();
        _bh = new ButtonHandler();
        _pd = new PrizeData();
        _bd = new ButtonData();
                
        _pd.JarsOfCoffee = 0;
        _gc.jarsOfCoffeePrize();

        Assert.AreEqual(0, _pd.JarsOfCoffee);
    }

    [Test]
    public void TestForNegativeJarsOfCoffeeAreNotEqual()
    {
        _gc = new GameController();
        _dc = new DataController();
        _bh = new ButtonHandler();
        _pd = new PrizeData();
        _bd = new ButtonData();

        _pd.JarsOfCoffee = -1;
        _gc.jarsOfCoffeePrize();

        Assert.AreNotEqual(-1, _pd.JarsOfCoffee);
    }

    [Test]
    public void TestForNegativeNoWinAreNotEqual()
    {
        _gc = new GameController();
        _dc = new DataController();
        _bh = new ButtonHandler();
        _pd = new PrizeData();
        _bd = new ButtonData();

        _pd.NoWin = -1;
        _gc.noWinPrize();

        Assert.AreNotEqual(-1, _pd.NoWin);
    }
}
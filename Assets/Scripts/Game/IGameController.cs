using System.Collections;
using System.Collections.Generic;

public interface IGameController {

    void TurnWheel();

    float CheckWheelFinalStop(float randomFinalAngle);

    void CheckForPrize(int clickNum);

    void GiveAwardIfAvailable(int randNum);

    void jarsOfCoffeePrize();

    void noWinPrize();

    void poundPrize();
}

using System;
using CardGameModel;

namespace Assignment1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var hand = new Hand();

            #region Generate Cars

            var cars = hand.GenerateCars(30);
            Console.WriteLine("------------ All registered cars ------------\n");
            Console.WriteLine(cars.ToString<Car>());

            #endregion

            #region Compare Cards
            
            Console.WriteLine("------------ Compareing cards ------------\n");
            var carComparison = cars[0].CompareCarProperties(cars[1]);

            Console.WriteLine(hand.CompareCards(carComparison));
            var detailedCardComparison = hand.CompareCardsByCarProperties(carComparison, cars[0], cars[1]);
            Console.WriteLine($"The detailed card comparison is: {detailedCardComparison}");

            #endregion

            #region Sort Cars

            cars = cars.Sort();
            Console.WriteLine("------------ Cars sorted by Max Speed ------------\n");
            Console.WriteLine(cars.ToString<Car>());

            #endregion

            #region Filter Cars By Max Speed

            const int maxSpeed = 300;
            Console.WriteLine($"------------ Cars with max speed higher than {maxSpeed} ------------\n");
             
            var carsByMaxSpeed = hand.FilterCarsByMaxSpeed(maxSpeed);

            Console.WriteLine(carsByMaxSpeed.ToString<Car>());

            #endregion

            Console.ReadLine();
        }
    }
}

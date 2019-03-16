using System.Linq;
using Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardUnitTest
{
    [TestClass]
    public class CarUnitTest
    {
        [TestMethod]
        public void TestGenerateCars_CarsAreSuccessfullyGenerated()
        {
            // arrange
            var hand = new Hand();
            const int numberOfCarsToGenerate = 30;

            // act
            var cars = hand.GenerateCars(numberOfCarsToGenerate);

            // assert
            Assert.AreEqual(numberOfCarsToGenerate, cars.Count);

        }

        [TestMethod]
        public void TestSort_CarsAreSortedByMaxSpeed()
        {
            // arrange
            var hand = new Hand();
            var cars = hand.GenerateCars(20);
            var expectedList = cars.OrderByDescending(c => c.MaxSpeed).ToList();
            // act
            cars = cars.Sort();

            // assert
            Assert.IsTrue(expectedList.SequenceEqual(cars));
        }
        
        [TestMethod]
        public void TestFilterCarsByMaxSpeed_CarsAreFilteredByMaxSpeed()
        {
            // arrange
            var hand = new Hand();
            var cars = hand.GenerateCars(20);
            const int maxSpeed = 300;

            // act
            var fileteredCars = hand.FilterCarsByMaxSpeed(maxSpeed);

            // assert
            Assert.IsTrue(cars.Any(c => c.MaxSpeed < maxSpeed));
            Assert.IsFalse(fileteredCars.Any(c => c.MaxSpeed < maxSpeed));
        }

        [TestMethod]
        public void TestCompareCarProperties_CarPropertiesAreSuccessfullyCompared()
        {
            // arrange
            var hand = new Hand();
            var car1 = new Car
            {
                Acceleration = 6.0,
                Cylinders = 6,
                EngineSize = 6000.56,
                HorsePower = 654.23,
                MaxSpeed = 210.0
            };

            var car2 = new Car
            {
                Acceleration = 10.0,
                Cylinders = 6,
                EngineSize = 2996.89,
                HorsePower = 300,
                MaxSpeed = 250.0
            };

            hand.AddCar(car1);
            hand.AddCar(car2);
        
            // act
            var cardComparison = car1.CompareCarProperties(car2);

            // assert
            Assert.AreEqual(cardComparison.Item1, -1); //max speed -> car2 > car1
            Assert.AreEqual(cardComparison.Item2, -1); //acceleration -> car2 > car1
            Assert.AreEqual(cardComparison.Item3, 0); //cylinders -> car2 = car1
            Assert.AreEqual(cardComparison.Item4, 1); //engine size -> car2 < car1
            Assert.AreEqual(cardComparison.Item5, 1); //horse power -> car2 < car1

        }

        [TestMethod]
        public void TestCompareCards_WinnerAndLooserAreSuccessfullySelected()
        {
            // arrange
            var hand = new Hand();
            var car1 = new Car
            {
                Acceleration = 6.0,
                Cylinders = 6,
                EngineSize = 6000.56,
                HorsePower = 654.23,
                MaxSpeed = 210.0
            };

            var car2 = new Car
            {
                Acceleration = 10.0,
                Cylinders = 7,
                EngineSize = 2996.89,
                HorsePower = 300,
                MaxSpeed = 250.0
            };

            hand.AddCar(car1);
            hand.AddCar(car2);
            var carPropertiesComparison = car1.CompareCarProperties(car2);

            // act
            var cardsComparison = hand.CompareCards(carPropertiesComparison);

            // assert
            Assert.AreEqual(cardsComparison, "Second card is the winner. Winner Score 3. Looser score: 2\n");
        }


        [TestMethod]
        public void TestCompareCards_WinnerAndLooserTie()
        {
            // arrange
            var hand = new Hand();
            var car1 = new Car
            {
                Acceleration = 6.0,
                Cylinders = 6,
                EngineSize = 6000.56,
                HorsePower = 654.23,
                MaxSpeed = 210.0
            };

            var car2 = new Car
            {
                Acceleration = 10.0,
                Cylinders = 6,
                EngineSize = 2996.89,
                HorsePower = 300,
                MaxSpeed = 250.0
            };

            hand.AddCar(car1);
            hand.AddCar(car2);
            var carPropertiesComparison = car1.CompareCarProperties(car2);

            // act
            var cardsComparison = hand.CompareCards(carPropertiesComparison);

            // assert
            Assert.AreEqual(cardsComparison, "First card and Second card tied!\n");

        }
    }
}

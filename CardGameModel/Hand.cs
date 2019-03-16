using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameModel
{
    public class Hand
    {
        private readonly IList<Car> _cars;

        public Hand(IList<Car> cars)
        {
            _cars = cars;
        }

        public Hand()
        {
            _cars = new List<Car>();
        }

        public void AddCar(Car car)
        {
            _cars.Add(car);
        }

        public IList<Car> GetCarsInHand()
        {
            return _cars;
        }

        #region GenerateCars

        public IList<Car> GenerateCars(int numberOfDesiredCars)
        {
            var random = new Random();
            var index = 0;
            while (index < numberOfDesiredCars)
            {
                _cars.Add(new Car
                {
                    Acceleration = GenerateRandomDouble(Car.MaximumAcceleration, Car.MinimumAcceleration, random),
                    Cylinders = random.Next(Car.MinimumCylinders, Car.MaximumCylinders),
                    EngineSize = GenerateRandomDouble(Car.MaximumEngineSize, Car.MinimumEngineSize, random),
                    HorsePower = GenerateRandomDouble(Car.MaximumHorsePower, Car.MinimumHorsePower, random),
                    MaxSpeed = GenerateRandomDouble(Car.MaximumMaxSpeed, Car.MinimumMaxSpeed, random),
                });
                index++;
            }

            return _cars;
        }

        private static double GenerateRandomDouble(double maximum, double minimum, Random random)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        #endregion

        #region Filter By Max Speed

        public IList<Car> FilterCarsByMaxSpeed(double maxSpeed)
        {
            return _cars.Where(c => c.MaxSpeed > maxSpeed).ToList();
        }

        #endregion

        #region Compare Cards

        public string CompareCards(
            (int maxSpeedComparison, int accelerationComparison, int cylinderComparison, int engineSizeComparison, int
                horsePowerComparison) comparisonTuple)
        {
            // if tuple contains more 1s - first card is the winner
            // if tuple contains more -1s - Second card is the winner
            var comparisonList = new List<int> {
                comparisonTuple.accelerationComparison, comparisonTuple.cylinderComparison,
                comparisonTuple.engineSizeComparison, comparisonTuple.horsePowerComparison,
                comparisonTuple.maxSpeedComparison
            };

            var grouped = comparisonList.Where(x => x != 0).ToLookup(x => x);

            var winnerScore = grouped.Max(x => x.Count());
            var looserScore = grouped.Min(x => x.Count());

            if (winnerScore == looserScore)
            {
                return "First card and Second card tied!\n";
            }

            var winnerCard = grouped.Where(x => x.Count() == winnerScore)
                .Select(x => x.Key).ToList()[0];

            var resultString = $"card is the winner. Winner Score {winnerScore}. Looser score: {comparisonList.Count - winnerScore}\n";


            return winnerCard == 1 ? $"First {resultString}" : $"Second {resultString}";
        }

        public string CompareCardsByCarProperties(
           (int maxSpeedComparison, int accelerationComparison, int cylinderComparison, int engineSizeComparison, int
               horsePowerComparison) comparisonTuple, Car firstCar, Car secondCar)
        {
            IDictionary<string, Tuple<int, double, double>> comparisonDictionary = new Dictionary<string, Tuple<int, double, double>>()
            {
                {"Max Speed", new Tuple<int, double, double>(comparisonTuple.maxSpeedComparison, firstCar.MaxSpeed, secondCar.MaxSpeed)},
                {"Engine Size", new Tuple<int, double, double>(comparisonTuple.engineSizeComparison, firstCar.EngineSize, secondCar.EngineSize)},
                {"Acceleration", new Tuple<int, double, double>(comparisonTuple.accelerationComparison, firstCar.Acceleration, secondCar.Acceleration)},
                {"Cylinders", new Tuple<int, double, double>(comparisonTuple.cylinderComparison, firstCar.Cylinders, secondCar.Cylinders)},
                {"Horse Power", new Tuple<int, double, double>(comparisonTuple.horsePowerComparison, firstCar.HorsePower, secondCar.HorsePower)}
            };

            var sb = new StringBuilder();

            foreach (var comparation in comparisonDictionary)
            {
                sb.Append(FormatComparison(comparation));
            }

            return sb.ToString();
        }

        public string FormatComparison(KeyValuePair<string, Tuple<int, double, double>> keyValuePair)
        {
            var tuple = keyValuePair.Value;
            var propertyName = keyValuePair.Key;

            switch (tuple.Item1)
            {
                case 0:
                    return $"First car's {propertyName} {tuple.Item2:n2} is equal to second car's {propertyName} {tuple.Item3:n2}.\n";
                case -1:
                    return $"First car's {propertyName} {tuple.Item2:n2} is lower than second car's {propertyName} {tuple.Item3:n2}.\n";
                case 1:
                    return $"First car's {propertyName} {tuple.Item2:n2} is higher than second car's {propertyName} {tuple.Item3:n2}.\n";
                default:
                    return "";
            }
        }

        #endregion

    }
}
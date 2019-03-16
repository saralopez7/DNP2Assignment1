
namespace Cards
{
    public class Car 
    {
        public double MaxSpeed { get; set; }
        public double EngineSize { get; set; }
        public double HorsePower { get; set; }
        public double Acceleration { get; set; }
        public int Cylinders { get; set; }

        #region Maximum and Minimum constants
        
        // Values are measured in Km/h
        public const double MaximumMaxSpeed = 435.31;
        public const double MinimumMaxSpeed = 104.60;

        // Values are measured in seconds
        public const double MaximumAcceleration = 10.97;
        public const double MinimumAcceleration = 2.0;

        // Values are measured in ccm
        public const double MaximumEngineSize = 8400;
        public const double MinimumEngineSize = 624;

        // Range from 0 to 20 - only even numbers and number 5
        public const int MaximumCylinders = 20;
        public const int MinimumCylinders = 0;

        public const double MaximumHorsePower = 808;
        public const double MinimumHorsePower = 120;

        #endregion

        #region Compare Cars

        public (int, int, int, int, int) CompareCarProperties(Car other)
        {
            var comparisonTuple = (
                maxSpeedComparison: MaxSpeed.CompareTo(other.MaxSpeed), 
                accelerationComparison: Acceleration.CompareTo(other.Acceleration),
                cylinderComparison: Cylinders.CompareTo(other.Cylinders), 
                engineSizeComparison: EngineSize.CompareTo(other.EngineSize), 
                horsePowerComparison: HorsePower.CompareTo(other.HorsePower)
                );
          
            return comparisonTuple;
        }

        #endregion

        public override string ToString()
        {
            return $"Max Speed: {MaxSpeed:n2} \r\nEngine Size: {EngineSize:n2} \r\n" +
                   $"HorsePower: {HorsePower:n2} \r\n{nameof(Acceleration)}: {Acceleration:n2}" +
                   $"\r\n{nameof(Cylinders)}: {Cylinders}";
        }
    }
}

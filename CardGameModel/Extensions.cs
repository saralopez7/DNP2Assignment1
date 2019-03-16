using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameModel
{
    public static class Extensions
    {
        #region Sort Cars By Max Speed

        public static IList<Car> Sort(this IList<Car> cars)
        {
            return cars.OrderByDescending(c => c.MaxSpeed).ToList();
        }

        #endregion

        public static string ToString<T>(this IEnumerable<T> list)
        {
            var sb = new StringBuilder();

            foreach (var item in list)
            {
                sb.Append(item + "\n\n");
            }

            return sb.ToString();
        }
    }
}
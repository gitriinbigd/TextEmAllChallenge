namespace TextEmAll
{
    using System;
    using System.Linq;

    public interface IChallenge2Service
    {
        /// <summary>
        /// Value returned when there is no calculable distance between letters 
        /// </summary>
        public const int NO_RESULT = -1;
        int MaxDistance(string input);
    }

    public class Challenge2Service : IChallenge2Service
    {
        /// <summary>
        /// Returns the maximim distance for a pair of letters in the string input
        /// 
        /// Sample Input: "gbcjdbha"
        /// Sample Output: 7
        /// Explanation: There are 7 letters between 'b' and 'j' and 'b' comes before 'j' alphabetically and the index of 'j' > index of 'b'
        /// 
        /// Boundary Cases:
        ///     Sample Input: "ab"
        ///     Expected Output: 0 
        ///     Explanation: there are 0 letters between 'a' and 'b' because they are adjacent 
        ///     
        ///     Sample Input: "az"
        ///     Expected Output: 24
        ///     Explanation: there are 24 letters between 'a' and 'z' nothwithstanding the 'a' and 'z'
        ///     
        ///     Sample Input: ""
        ///     Expected Output: -1
        ///     Explanation: return a 'magic number' indicating there is no valid distance calculable 
        /// 
        /// </summary>
        /// <param name="input">The string of characters</param>
        /// <returns>Te maximum distance calculated as the largest difference between any input[i] and input[j] where i < j and input[i] comes before input[j] in the alphabet</returns>
        public int MaxDistance(string input)
        {
            // assume we'll find nothing and return a rational value that indicates as much 
            int maxDistance = IChallenge2Service.NO_RESULT;

            // convert the string to an array of characters making it lowercase so we can use the ASCII value to determine "distance" 
            char[] stringChars = (input ?? String.Empty) // ensure our effort is null-safe 
                .Where(Char.IsLetter)                    // only consider letters 
                .Select(Char.ToLower)                    // convert to lower so our math is consistent 
                .ToArray();                              // return our result as an array of characters 

            // iterate the array considering each entry exept the last since there's nothing after it 
            for(int charIndex = 0; charIndex <= stringChars.Length - 1; charIndex++)
            {
                // the "next" possible character
                int min = stringChars[charIndex];

                // get the next largest character downstream using linq
                int max = stringChars.Skip(charIndex + 1) // consider only downstream array entries 
                    .Where(candidate => candidate > min)  // limit to only those larger than our current min 
                    .DefaultIfEmpty()                     // handle the possibility of no results 
                    .Max();                               // return the largest qualifying value 

                // calculate the distance by counting "letters" between our min and max adjusting for adjacency 
                int distance = max - min - 1;

                // if our calculated distance is the new max, assign accordingly 
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }

            // return the fruits of our labor 
            return maxDistance;
        }
    }
}

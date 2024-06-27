using System.ComponentModel.DataAnnotations;

namespace HWTechnicalTest.FTApi
{
    public class FTRangeParameters
    {
        const int MAX_OFFSET = 3000; // max offset is 3000
        const int MAX_COUNT = 150; // max count is 150


        /// <summary>
        /// offset is the index of the first element to return
        /// </summary>
        [Range(0, MAX_OFFSET, ErrorMessage = "Offset must be between 0 and 3000")]
        public int Offset { get; set; }

        /// <summary>
        /// count is the number of elements to return
        /// </summary>
        [Range(1, MAX_COUNT, ErrorMessage = $"Count must be between 1 and 150")]
        public int Count { get; set;}

        public override string ToString()
        {
            return $"{Offset}-{Offset+Count-1}";
        }
    }
}

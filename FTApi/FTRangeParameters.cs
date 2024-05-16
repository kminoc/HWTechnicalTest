using ZstdSharp.Unsafe;

namespace HWTechnicalTest.FTApi
{
    public class FTRangeParameters
    {
        const int MAX_OFFSET = 3000; // max offset is 3000
        const int MAX_COUNT = 150; // max count is 150

        private int _offset;
        private int _count;

        /// <summary>
        /// offset is the index of the first element to return
        /// </summary>
        public int Offset 
        {
            get => _offset;
            set => _offset = Math.Max(0, Math.Min(MAX_OFFSET, value)); // Offset must be be between 0 and MAX_OFFSET
        }

        /// <summary>
        /// count is the number of elements to return
        /// </summary>
        public int Count
        { 
            get => _count; 
            set => _count = Math.Max(1,Math.Min(MAX_COUNT, value)); // Count must be between 1 and MAX_COUNT
        }

        public override string ToString()
        {
            return $"{Offset}-{Offset+Count-1}";
        }
    }
}

using Microsoft.AspNetCore.Http.Extensions;
using System.Collections.Specialized;

namespace HWTechnicalTest.FTApi
{
    public class FTApiParameters
    {
        private int _sort = 1; // default sort is 1 -> sort by publication date desc
        private int _daysPublishSince = 31; // default is 0 -> no filter on publication date

        private List<int> DAYS_PUBLISH_SINCE_VALUES = new List<int>(){1, 3, 7, 14, 31}; // max days publish since is 31

        public FTRangeParameters Range { get; set; }
        public int Sort 
        { 
            get => _sort; 
            set => _sort = value < 0 || value > 2 ? 1 : value; // sort must be 0,1 or 2, if not 1 is used
        }
        public string INSEELocation { get; set; }

        public int DaysPublishSince 
        { 
            get => _daysPublishSince; 
            set => _daysPublishSince = DAYS_PUBLISH_SINCE_VALUES.Contains(value) ? value : 31;
        }

        public DateTime? MaxCreationDate { get; set; }

        public override string ToString()
        {
            var qb = new QueryBuilder();

            if(Range != null) qb.Add("range", Range.ToString());
            if(!String.IsNullOrEmpty(INSEELocation)) qb.Add("commune", INSEELocation);
            if (MaxCreationDate.HasValue) qb.Add("maxCreationDate", MaxCreationDate.Value.ToString("yyyy-MM-dd'T'HH:mm:ssK"));
            qb.Add("publieeDepuis", DaysPublishSince.ToString());

            return qb.ToString();            
        }
    }
}

namespace HWTechnicalTest.FTApi
{
    public class FTAccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public DateTime? expires_at { get; set; }
        public bool Expired => expires_at < DateTime.Now;
    }
}

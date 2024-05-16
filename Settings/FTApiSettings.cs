namespace HWTechnicalTest.Settings
{
    public class FTApiSettings
    {
        /// <summary>
        /// login endpoint url
        /// </summary>
        public string LoginUrl { get; set; }

        /// <summary>
        /// Scope for the login
        /// </summary>
        public string LoginScope { get; set; }

        /// <summary>
        /// Grant type for the login
        /// </summary>
        public string LoginGrantType { get; set; }

        /// <summary>
        /// Client Id for the login
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client Secret for the login
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Api endpoint url
        /// </summary>
        public string ApiUrl { get; set; }
       
        /// <summary>
        /// List of location's INSEECodes to fetch 
        /// </summary>
        public List<string> LocationsInsee { get; set; }

        /// <summary>
        /// how many days the offers has been published, can be 1, 3, 7, 31
        /// </summary>
        public int DaysPublishSince { get; set; }
    }
}

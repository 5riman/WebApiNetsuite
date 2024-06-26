using WebApiNetsuite.com.netsuite.webservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;


namespace WebApiNetsuite.NetSuite
{
    public class NSClient : IDisposable
    {
        private NetSuiteService _service;
        private string accountId;
        private string consumerKey;
        private string consumerSecret;
        private string tokenId;
        private string tokenSecret;

        /// <value>Proxy class that abstracts the communication with the 
        /// NetSuite Web Services. All NetSuite operations are invoked
        /// as methods of this class.</value>
        public NetSuiteService Service
        {
            get
            {
                _service.tokenPassport = CreateTokenPassport();
                return _service;
            }
        }

        /// <value>Flag that indicates whether the user is currently 
        /// authentciated, and therefore, whether a valid session is 
        /// available</value>
        public bool IsAuthenticated { get; private set; }
        public void Dispose()
        {
            if (IsAuthenticated)
            {
                _service.logout();
                Status status = (_service.logout()).status;
                if (status.isSuccess == true)
                {
                }
            }
            _service.Dispose();
        }

        /// <value>A NameValueCollection that abstracts name/value pairs from
        /// the app.config file in the Visual .NET project. This file is called
        /// [AssemblyName].exe.config in the distribution</value>
        public System.Collections.Specialized.NameValueCollection DataCollection { get; private set; }

        /// <value>Default page size used for search in this application</value>
        public int PageSize { get; private set; }

        /// Set up request level preferences as a SOAP header
        public Preferences Prefs { get; private set; }
        public SearchPreferences SearchPreferences { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public NSClient(string accountId, string consumerKey, string consumerSecret, string tokenKey, string tokenSecret)
        {
            this.accountId = accountId;
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.tokenId = tokenKey;
            this.tokenSecret = tokenSecret;

            IsAuthenticated = false;
            PageSize = 1000;

            // Reference to config file that contains sample data. This file
            // is named App.config in the Visual .NET project or, <AssemblyName>.exe.config
            // in the distribution
            DataCollection = System.Configuration.ConfigurationManager.AppSettings;

            // Instantiate the NetSuite web services
            _service = new DataCenterAwareNetSuiteService(this.accountId, false);

            _service.Timeout = 1000 * 60 * 60 * 1;//7200000 mili seconds to 2hrs

            //Enable cookie management
            Uri myUri = new Uri("https://webservices.netsuite.com");
            _service.CookieContainer = new CookieContainer();
        }

        /// <summary>
        /// <p>This function builds the Pereferences and SearchPreferences in the SOAP header. </p>
        /// </summary>
        public void SetPreferences()
        {
            // Set up request level preferences as a SOAP header
            Prefs = new Preferences();
            _service.preferences = Prefs;
            SearchPreferences = new SearchPreferences();
            _service.searchPreferences = SearchPreferences;

            // Preference to ask NS to treat all warnings as errors
            Prefs.warningAsErrorSpecified = true;
            Prefs.warningAsError = false;
            Prefs.ignoreReadOnlyFieldsSpecified = true;
            Prefs.ignoreReadOnlyFields = true;
            
            SearchPreferences.pageSize = PageSize;
            SearchPreferences.pageSizeSpecified = true;
            // Setting this bodyFieldsOnly to true for faster search times on Opportunities
            SearchPreferences.bodyFieldsOnly = true;
            PrepareLoginPassport();
        }
        private void PrepareLoginPassport()
        {
            _service.tokenPassport = CreateTokenPassport();
        }
        /// <summary>
        /// Update search preference to either return body fields, return columns or full records
        /// </summary>
        /// <param name="bodyFieldsOnly"></param>
        /// <param name="returnColumns"></param>
        public void SetSearchPreferences(bool bodyFieldsOnly, bool returnColumns)
        {
            _service.searchPreferences.bodyFieldsOnly = bodyFieldsOnly;
            _service.searchPreferences.returnSearchColumns = returnColumns;
        }

        public void SetSearchPageSize(int pageSize)
        {
            _service.searchPreferences.pageSize = pageSize;
            _service.searchPreferences.pageSizeSpecified = true;
        }

        /// <summary>
        /// <p>Processes the status object and prints the status details</p>
        /// </summary>
        public String GetStatusDetails(Status status)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < status.statusDetail.Length; i++)
            {
                sb.Append("[Code=" + status.statusDetail[i].code + "] " + status.statusDetail[i].message + "\n");
            }
            return sb.ToString();
        }

        public TokenPassport CreateTokenPassport()
        {
            string nonce = ComputeNonce();
            long timestamp = ComputeTimestamp();
            TokenPassportSignature signature = ComputeSignature(accountId, consumerKey, consumerSecret, tokenId, tokenSecret, nonce, timestamp);
            TokenPassport tokenPassport = new TokenPassport();
            tokenPassport.account = accountId;
            tokenPassport.consumerKey = consumerKey;
            tokenPassport.token = tokenId;
            tokenPassport.nonce = nonce;
            tokenPassport.timestamp = timestamp;
            tokenPassport.signature = signature;
            return tokenPassport;
        }

        private string ComputeNonce()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] data = new byte[20];
            rng.GetBytes(data);
            int value = Math.Abs(BitConverter.ToInt32(data, 0));
            return value.ToString();
        }

        private long ComputeTimestamp()
        {
            return ((long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }

        private TokenPassportSignature ComputeSignature(string compId, string consumerKey, string consumerSecret,
                                        string tokenId, string tokenSecret, string nonce, long timestamp)
        {
            string baseString = compId + "&" + consumerKey + "&" + tokenId + "&" + nonce + "&" + timestamp;
            string key = consumerSecret + "&" + tokenSecret;
            string signature = "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(key);
            byte[] baseStringBytes = encoding.GetBytes(baseString);

            using (var hmacSha256 = new HMACSHA256(keyBytes))
            {
                byte[] hashBaseString = hmacSha256.ComputeHash(baseStringBytes);
                signature = Convert.ToBase64String(hashBaseString);
            }

            TokenPassportSignature sign = new TokenPassportSignature();
            sign.algorithm = "HMAC-SHA256";
            sign.Value = signature;
            return sign;
        }

        //private ApplicationInfo CreateApplicationId()
        //{
        //    ApplicationInfo applicationInfo = new ApplicationInfo();
        //    applicationInfo.applicationId = DataCollection["login.appId"];
        //    return applicationInfo;
        //}

        /// <summary>
        /// Use to get default values for deletion reason
        /// </summary>
        /// <returns>DeletionReason with some default values</returns>
        //public DeletionReason GetDefaultDeletionReason()
        //{
        //    DeletionReason deletionReason = new DeletionReason();
        //    RecordRef deletionReasonCodeRef = new RecordRef();
        //    deletionReasonCodeRef.internalId = "1";
        //    deletionReason.deletionReasonCode = deletionReasonCodeRef;
        //    deletionReason.deletionReasonMemo = "Deleted from Sample Apps.";
        //    return deletionReason;
        //}
    }

    class OverrideCertificatePolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint, System.Security.Cryptography.X509Certificates.X509Certificate certificate, WebRequest request, int certificateProblem)
        {
            return true;
        }
    }

    /// <summary>    
	/// Since 12.2 endpoint accounts are located in multiple datacenters with different domain names.
    /// In order to use the correct one, wrap the Service and get the correct domain first.
	///
	/// See getDataCenterUrls WSDL method documentation in the Help Center.	 
    /// </summary>
    class DataCenterAwareNetSuiteService : NetSuiteService
    {
        private System.Uri OriginalUri;

        public DataCenterAwareNetSuiteService(string account, bool doNotSetUrl)
            : base()
        {
            OriginalUri = new System.Uri(this.Url);
            if (account == null || account.Length == 0)
                account = "empty";
            if (!doNotSetUrl)
            {
                DataCenterUrls urls = getDataCenterUrls(account).dataCenterUrls;
                Uri dataCenterUri = new Uri(urls.webservicesDomain + OriginalUri.PathAndQuery);
                this.Url = dataCenterUri.ToString();
            }
        }

        public void SetAccount(string account)
        {
            if (account == null || account.Length == 0)
                account = "empty";

            this.Url = OriginalUri.AbsoluteUri;
            DataCenterUrls urls = getDataCenterUrls(account).dataCenterUrls;
            Uri dataCenterUri = new Uri(urls.webservicesDomain + OriginalUri.PathAndQuery);
            this.Url = dataCenterUri.ToString();
        }
    }

}
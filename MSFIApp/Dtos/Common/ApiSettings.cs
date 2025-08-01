namespace MSFIApp.Dtos.Common
{
    public class ApiSettings
    {
        /// <summary>
        /// تعداد درخواست مجدد وقتی خطایی در برقراری ارتباط
        /// با سرور پیش میاد
        /// </summary>
        public int maxRetryAttempts { get; set; }
        /// <summary>
        /// مدت زمان فاصله بین هر درخواست مجدد
        /// </summary>
        public int delayMilliseconds { get; set; }
        public string BaseUrl { get; set; }
        public string Token_Key { get; set; }
    }
}

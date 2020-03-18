using System;

namespace PlattSampleApp.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string StatusCode { get; set; }
        public string Reason { get; set; }
    }
}
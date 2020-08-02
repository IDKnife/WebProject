using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork.Services.Interfaces
{
    public class ServiceOperationResult
    {
        public bool Result { get; }
        public string MessageResult { get; }

        public ServiceOperationResult(bool result, string messageResult)
        {
            Result = result;
            MessageResult = messageResult;
        }
    }
}

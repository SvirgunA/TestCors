using TestCors.Common.Enums;

namespace TestCors.Common
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            Status = EServiceResultStatus.Error;
        }

        public EServiceResultStatus Status { get; set; }
        public string Message { get; set; }

        public bool Failed => Status != EServiceResultStatus.Success;
        public bool Succeded => Status == EServiceResultStatus.Success;

    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Entity { get; set; }

        public static ServiceResult<T> OK()
        {
            return new ServiceResult<T> { Status = EServiceResultStatus.Success, Entity = default(T) };
        }

        public static ServiceResult<T> Error(string msg)
        {
            return new ServiceResult<T> { Status = EServiceResultStatus.Error, Message = msg};
        }
    }
}

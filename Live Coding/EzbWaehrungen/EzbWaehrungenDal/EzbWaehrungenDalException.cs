
namespace EzbWaehrungenDal
{
    [Serializable]
    public class EzbWaehrungenDalException : Exception
    {
        public EzbWaehrungenDalException()
        {
        }

        public EzbWaehrungenDalException(string? message) : base(message)
        {
        }

        public EzbWaehrungenDalException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
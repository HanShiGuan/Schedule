using System.Threading.Tasks;
using Rongzi.Entity;

namespace Rongzi.Mvc.Infrastructure
{
    /// <summary>
    /// Response helper class
    /// </summary>
    public static class ResponseHelper
    {
        /// <summary>
        /// Check response context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool HasContent<T>(this Task<ResponseContext<T>> response)
        {
            return response != null
                && response.Result != null
                && response.Result.Content != null;
        }

        /// <summary>
        /// Check response head
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool HasHead<T>(this Task<ResponseContext<T>> response)
        {
            return response != null
                && response.Result != null
                && response.Result.Head != null;
        }

        public static bool HasHeadAndContent<T>(this Task<ResponseContext<T>> response)
        {
            return response != null
                && response.Result != null
                && response.Result.Head != null
                && response.Result.Content != null;
        }
    }
}

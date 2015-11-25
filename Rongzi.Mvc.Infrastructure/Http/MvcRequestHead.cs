using Rongzi.Entity;

namespace Rongzi.Mvc.Infrastructure
{
    /// <summary>
    /// Request Head used by mvc
    /// </summary>
    public class MvcRequestHead : RequestHead
    {
        public MvcRequestHead()
        {
            AppType = 0;
            Token = string.Empty;
            Version = string.Empty;
        }
    }
}

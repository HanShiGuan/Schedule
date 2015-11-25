using Rongzi.Entity;

namespace Rongzi.Mvc.Infrastructure
{
    public class MvcRequestContext<T> : RequestContext<T>
    {
        public MvcRequestContext()
        {
            Head = new MvcRequestHead();
        }

    }
}

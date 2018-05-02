using Autofac;

namespace TMall.Framework.ServiceLocation
{
    public class MyServiceLocator
    {
        private static IServiceLocator _autofacServiceLocator;

        private static readonly object Locker = new object();

        private MyServiceLocator()
        { 
            
        }

        public static void InitServiceLocator(IComponentContext container)
        {
            if (_autofacServiceLocator == null)
            {
                lock (Locker)
                {
                    if (_autofacServiceLocator == null)
                    {
                        _autofacServiceLocator = new AutofacServiceLocator(container);
                    }
                }
            }
        }

        public static IServiceLocator Current
        {
            get
            {
                return _autofacServiceLocator;
            }
        }
    }
}

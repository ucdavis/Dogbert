using System;

namespace Dogbert.Tests
{
    public class ContinuousIntegrationDeploymentHack
    {
        public ContinuousIntegrationDeploymentHack()
        {
            new NHibernate.ByteCode.Spring.ProxyFactoryFactory();
            new System.Data.SQLite.SQLiteException();

            throw new Exception("This class should never be called or instantiated");
        }
    }
}

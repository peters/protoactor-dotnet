namespace Proto.Router.Routers
{
    internal class ConsistentHashPoolRouterConfig : PoolRouterConfig
    {
        public ConsistentHashPoolRouterConfig(int poolSize)
            : base(poolSize)
        {
        }

        public override RouterState CreateRouterState()
        {
            return new ConsistentHashRouterState();
        }
    }
}
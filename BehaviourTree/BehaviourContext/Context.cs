namespace Coffee.BehaviourTree.Ctx
{
    internal class Context
    {
        public readonly Context parent;
        public readonly TreeBaseNode contextRoot;

        public Context(Context parent, TreeBaseNode contextRoot)
        {
            this.parent = parent;
            this.contextRoot = contextRoot;
        }
    }
}
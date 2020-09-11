namespace Coffee.BehaviourTree.Ctx
{
    internal class ContextBlock
    {
        public TreeBaseNode contextSubject;
    }
    
    internal class Context
    {
        private ContextBlock parentBlock;
        private ContextBlock currentBlock;
    }
}
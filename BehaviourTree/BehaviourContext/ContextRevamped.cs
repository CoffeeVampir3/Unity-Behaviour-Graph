namespace Coffee.BehaviourTree.Ctx
{
    internal class ContextBlock
    {
        public ContextBlock parentBlock;
        public TreeBaseNode contextRoot;

        public ContextBlock(ContextBlock parentBlock, TreeBaseNode contextRoot)
        {
            this.parentBlock = parentBlock;
            this.contextRoot = contextRoot;
        }
    }
    
    internal class ContextWalker
    {
        private ContextBlock currentBlock;

        public void SetContextPointer(ContextBlock block)
        {
            currentBlock = block;
        }

        public bool Unwind()
        {
            if (currentBlock.parentBlock == null) 
                return false;
            
            currentBlock = currentBlock.parentBlock;
            return true;
        }

        public TreeBaseNode GetContextNode()
        {
            return currentBlock?.contextRoot;
        }
    }
}
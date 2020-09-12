namespace Coffee.BehaviourTree.Ctx
{
    internal class ContextWalker
    {
        private Context current;

        public void SetContextPointer(Context block)
        {
            current = block;
        }

        public bool Unwind()
        {
            if (current.parent == null) 
                return false;
            
            current = current.parent;
            return true;
        }

        public void Reset()
        {
            current.contextRoot.Reset();
        }

        public TreeBaseNode GetContextNode()
        {
            return current.contextRoot;
        }
    }
}

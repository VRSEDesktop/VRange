public class Selector<T> : Node<T>
{
    public Node<T>[] Nodes;

    public override Status Update(T context)
    {
        for(int i = 0; i < Nodes.Length; )
        {
            Status status = Nodes[i].Update(context);
            if (status == Status.Success)
                return Status.Success;
            else if(status == Status.Failed)
                ++i;
            //If status == Status.Running, call Updated method from the same node again
        }
        return Status.Failed;
    }
}
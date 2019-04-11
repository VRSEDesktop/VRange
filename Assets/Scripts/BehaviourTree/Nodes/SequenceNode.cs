using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence<T> : Node<T>
{
    public Node<T>[] Children;

    public override Status Update(T context)
    {
        for(int i = 0; i < Children.Length;)
        {
            Status status = Children[i].Update(context);
            if (status == Status.Failed)
                return Status.Failed;
            else if (status == Status.Success)
                ++i;
            //If status == Status.Running, call Updated method from the same node again
        }
        return Status.Success;
    }
}

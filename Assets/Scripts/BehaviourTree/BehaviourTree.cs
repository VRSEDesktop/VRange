using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    Running,
    Success,
    Failed
}

public class BehaviourTree<T>
{
    public Node<T> Root;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node<T>
{
    public abstract Status Update(T context);
}

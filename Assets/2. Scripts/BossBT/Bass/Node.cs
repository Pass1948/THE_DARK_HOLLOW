using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum NodeStatus { Success, Failure, Running }
public abstract class Node
{
    public string name { get; }
    protected bool started;
    public Node(string name = null) 
    {
        name = name ?? GetType().Name; 
    }

    public NodeStatus Tick(Blackboard bb, float dt)
    {
        if (!started) { started = true; OnEnter(bb); }
        var status = OnTick(bb, dt);
        if (status != NodeStatus.Running) { OnExit(bb); started = false; }
        return status;
    }

    public virtual void OnEnter(Blackboard bb) { }
    public virtual void OnExit(Blackboard bb) { }
    protected abstract NodeStatus OnTick(Blackboard bb, float dt);

    // 트리 전체 초기화가 필요할 때
    public virtual void ResetTree() 
    {
        started = false; 
    }

}

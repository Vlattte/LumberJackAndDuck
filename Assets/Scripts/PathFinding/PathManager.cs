using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathManager : MonoBehaviour
{
    Queue<PathRequest> requestQueue = new Queue<PathRequest>();
    PathRequest curRequest;

    static PathManager instance;
    PathFinder path_finder;

    bool isfindingPath;

    private void Awake()
    {
        instance = this;
        path_finder = GetComponent<PathFinder>();
    }

    public static void Requestpath(Vector2 start, Vector2 end, Action<Vector3[], bool> call_back)
    {
        PathRequest newRequest = new PathRequest(start, end, call_back);
        instance.requestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isfindingPath && requestQueue.Count > 0)
        {
            curRequest = requestQueue.Dequeue();
            isfindingPath = true;
            path_finder.StartFindingPath(curRequest.start, curRequest.end);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool isSuccess)
    {
        curRequest.call_back(path, isSuccess);
        isfindingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 start;
        public Vector3 end;
        public Action <Vector3[], bool> call_back;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _call_back)
        {
            start = _start;
            end = _end; 
            call_back = _call_back;
        }
    }
}

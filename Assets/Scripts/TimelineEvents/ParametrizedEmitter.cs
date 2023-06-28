using UnityEngine;
using UnityEngine.Timeline;

namespace TimelineEvents
{
    public abstract class ParametrizedEmitter<T> : SignalEmitter
    {
        [SerializeField] private T _parameter;

        public T Parameter => _parameter;
    }
}
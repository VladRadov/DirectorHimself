using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventBus
{
    private static Dictionary<Type, List<IGlobalSubscriber>> _events = new Dictionary<Type, List<IGlobalSubscriber>>();

    public static void Subscribe(IGlobalSubscriber subscriber)
    {
        var typesSubscriber = GetSubscriberTypes(subscriber);

        foreach (var type in typesSubscriber)
        {
            if (_events.ContainsKey(type) == false)
                _events[type] = new List<IGlobalSubscriber>();

            _events[type].Add(subscriber);
        }
    }

    public static List<Type> GetSubscriberTypes(IGlobalSubscriber subscriber)
    {
        var typeSubscriber = subscriber.GetType();

        var typesSubscriber = typeSubscriber.GetInterfaces().Where
            (
                type => typeof(IGlobalSubscriber).IsAssignableFrom(type) && type != typeof(IGlobalSubscriber)
            ).ToList();

        return typesSubscriber;
    }

    public static void InvokeEvents<T>(Action<T> action) where T : IGlobalSubscriber
    {
        var subscribers = _events[typeof(T)];

        foreach (T subscriber in subscribers)
            action.Invoke(subscriber);
    }
}

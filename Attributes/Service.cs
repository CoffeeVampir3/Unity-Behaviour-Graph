using System;

namespace BehaviourGraph.Attributes
{
    /// <summary>
    /// <para>Configures this method or field to be used in the behaviour graph inspector.</para>
    /// Services must be a method with no parameters which returns a ServiceState.
    /// </summary>
    /// <example>
    /// <code>
    /// [Service]
    /// public ServiceState MoveToRandomPoint()
    /// {
    ///     transform.localPosition = Vector2.MoveTowards(
    ///     transform.localPosition, waypoint, 
    ///     speed * Time.deltaTime);
    ///     if (Vector2.Distance(waypoint, transform.localPosition) > .1f)
    ///         {
    ///            return ServiceState.Running;
    ///         }
    ///     return ServiceState.Complete;
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    public class Service : Attribute
    {
    }
}
using System;

namespace BehaviourGraph.Attributes
{
    /// <summary>
    /// <para>Configures this method or field to be used in the behaviour graph inspector.</para>
    /// Condition fields must be of type bool. Condition methods must
    /// return a bool and have no parameters.
    /// </summary>
    /// <example>
    /// <code>
    /// [Condition]
    /// private bool isPlayerNearby = false;
    /// [Condition]
    /// private bool IsPlayerNearby() => false;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class Condition : Attribute
    {
    }
}
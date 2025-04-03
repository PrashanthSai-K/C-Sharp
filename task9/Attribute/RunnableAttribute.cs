using System;


namespace task9.Attribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class RunnableAttribute : System.Attribute
{
}

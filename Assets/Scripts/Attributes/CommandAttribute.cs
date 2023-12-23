
using System;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
sealed class CommandAttribute : Attribute
{
    public string Name { get; }
    public string Desctiption { get; }

    public CommandAttribute(string name, string description)
    {
        Name = name;
        Desctiption = description;
    }
}
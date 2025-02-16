using System.Reflection;

namespace Bam.Shell
{
    public interface IMenuItem
    {
        object Instance { get; set; }
        Attribute? Attribute { get; set; }
        Type? AttributeType { get; set; }
        bool Selected { get; set; }
        string Selector { get; }
        string DisplayName { get; }
        MethodInfo MethodInfo { get; }
    }
}

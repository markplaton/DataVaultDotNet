namespace DataVaultDotNet.Core.Attributes;
/// <summary>
/// Attribute describing a Business Key
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ForeignKeyAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public Type DimensionType { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dimensionType"></param>
    public ForeignKeyAttribute(Type dimensionType)
    {
        DimensionType = dimensionType;
    }
}

using System.Runtime.Serialization;

namespace SW.TechnicalAssignment.Models
{
    public enum Gender
    {
        Other,
        [EnumMember(Value = "M")]
        Male,
        [EnumMember(Value = "F")]
        Female
    }
}
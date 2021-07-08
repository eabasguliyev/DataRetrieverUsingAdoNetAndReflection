using System;

namespace PopulatingTable_Using_AdoNet_Reflection.CustomAttributes
{
    public enum DatabaseGeneratedOption : byte
    {
        None = 1,
        Identity
    }


    [System.AttributeUsage(AttributeTargets.Property)]
    public class DatabaseGeneratedAttribute:System.Attribute
    {
        public DatabaseGeneratedOption Option { get; set; }
    }
}
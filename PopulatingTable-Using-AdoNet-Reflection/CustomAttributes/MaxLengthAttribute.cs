using System;

namespace PopulatingTable_Using_AdoNet_Reflection.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute:System.Attribute
    {
        public int Length { get; set; }
    }
}
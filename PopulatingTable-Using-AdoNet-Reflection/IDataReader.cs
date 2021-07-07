using System;

namespace PopulatingTable_Using_AdoNet_Reflection
{
    public interface IDataReader:IDisposable
    {
        object this[string k]
        {
            get;
        }

        bool Read();
    }
}
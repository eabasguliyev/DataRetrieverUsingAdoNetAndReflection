using System;

namespace PopulatingTable_Using_AdoNet_Reflection
{
    public class Car
    {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public decimal Engine { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"|{Vendor,20}|{Model,20}|{Engine,20}|{Year,20}|";
        }
    }
}
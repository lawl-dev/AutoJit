using System;
using AutoJITRuntime.Variants;

namespace AutoJITRuntime
{
    public class ReturnsAttribute : Attribute
    {
        public DataType DataType { get; set; }
        
        public ReturnsAttribute( DataType dataType ) {
            DataType = dataType;
        }
    }
}
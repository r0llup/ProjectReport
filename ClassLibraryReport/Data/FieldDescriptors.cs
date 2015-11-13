using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;

namespace ClassLibraryReport.Data
{
    [Serializable]
    public class FieldDescriptors : Datas<FieldDescriptor>
    {
        public FieldDescriptors()
        {
        }

        public FieldDescriptors(List<FieldDescriptor> fieldDescriptorList) :
            base(fieldDescriptorList)
        {
        }

        public FieldDescriptors(FieldDescriptors fieldDescriptors) :
            base(fieldDescriptors)
        {
        }

        public FieldDescriptors(SerializationInfo si, StreamingContext sc) :
            base(si, sc)
        {
        }
    }
}
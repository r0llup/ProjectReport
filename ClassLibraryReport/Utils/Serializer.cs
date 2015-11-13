using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassLibraryReport.Utils
{
    public static class Serializer
    {
        public static void SerializeObject(String filename, ISerializable objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            var bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public static ISerializable DeSerializeObject(String filename)
        {
            try
            {
                Stream stream = File.Open(filename, FileMode.Open);
                var bFormatter = new BinaryFormatter();
                var objectToSerialize = bFormatter.Deserialize(stream) as ISerializable;
                stream.Close();
                return objectToSerialize;
            }
            catch (SerializationException)
            {
                return null;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }
}
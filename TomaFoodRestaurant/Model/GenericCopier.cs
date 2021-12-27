using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TomaFoodRestaurant.Model
{
    public static class GenericCopier<T>
    {
        public static T DeepCopy(object objectToCopy)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, objectToCopy);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}

using Lorena.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;

namespace Lorena.Models.Rules
{
    public static class SerializationHelper
    {
        /// <summary>
        /// Génére un stream à partir d'une chaîne JSON
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static List<Soin> GenererListeSoin(string chaineJson)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Soin>));
            var listeSoin = (List<Soin>)ser.ReadObject(GenerateStreamFromString(chaineJson));

            return listeSoin;
        }

        public static T Deserialiser<T>(string chaineJson)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            var objet = (T)ser.ReadObject(GenerateStreamFromString(chaineJson));

            return objet;

        }

    }
}
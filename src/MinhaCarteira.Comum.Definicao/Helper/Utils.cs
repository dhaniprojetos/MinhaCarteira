﻿using System.IO;
using System.Xml.Serialization;

namespace MinhaCarteira.Comum.Definicao.Helper
{
    public static class Utils
    {
        public static T CreateDeepCopy<T>(this T obj)
        {
            using var ms = new MemoryStream();
            XmlSerializer serializer = new(obj.GetType());
            serializer.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(ms);
        }
    }
}

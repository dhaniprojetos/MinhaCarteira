using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MinhaCarteira.Comum.Definicao.Helper
{
    public static class Utils
    {
        public static T CreateDeepCopy<T>(this T obj)
        {
            if (ReferenceEquals(obj, null)) return default;

            using var ms = new MemoryStream();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(ms);
        }

        public static T JsonCreateDeepCopy<T>(this T obj)
        {
            if (obj is null) return default;
            var deserializeSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            return JsonConvert.DeserializeObject<T>(
                JsonConvert.SerializeObject(obj),
                deserializeSettings);
        }

        public static void Mapear<T, TU>(this T target, TU source)
        {
            var tprops = target.GetType().GetProperties();

            tprops.ToList().ForEach(prop =>
            {
                var sp = source.GetType().GetProperty(prop.Name);
                if (sp == null) return;
                var value = sp.GetValue(source, null);
                if (prop.CanWrite)
                    target.GetType().GetProperty(prop.Name)?
                        .SetValue(target, value, null);
            });
        }
    }
}

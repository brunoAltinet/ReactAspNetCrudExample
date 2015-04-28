using System.Collections.Generic;
using System.Xml.Serialization;

namespace Altinet.Collections
{
    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue>
        : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        /// <summary>
        /// Saves to an xml file
        /// </summary>
        /// <param name="fileName">File path of the new xml file</param>
        public void Save(string fileName)
        {
            using (var writer = new System.IO.StreamWriter(fileName))
            {
                var serializer = new XmlSerializer(GetType());
                serializer.Serialize(writer, this);
                writer.Flush();
            }
        }

        public static SerializableDictionary<TKey, TValue> Load(string fileName)
        {
            using (var stream = System.IO.File.OpenRead(fileName))
            {
                var serializer = new XmlSerializer(typeof(SerializableDictionary<TKey, TValue>));
                return serializer.Deserialize(stream) as SerializableDictionary<TKey, TValue>;
            }
        }


        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
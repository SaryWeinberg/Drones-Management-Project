using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using DO;
using DalApi;

namespace Dal
{
    public class XmlTools
    {
        /// <summary>
        /// Save list to xml file by serializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        public static void SaveListToXmlSerializer<T>(IEnumerable<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception)
            {
                throw new XmlFileLoadCreateException($"fail to create xml file: {filePath}");
            }
        }

        /// <summary>
        /// Load list from xml by serializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IEnumerable<T> LoadListFromXmlSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    IEnumerable<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(filePath, FileMode.Open);
                    list = (IEnumerable<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
            }
            catch (Exception)
            {
                throw new XmlFileLoadCreateException($"fail to load xml file: {filePath}");
            }
            throw new XmlFileLoadCreateException($"{filePath} not exist at the file");
        }

        /// <summary>
        /// Save list to xml by XElement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        public static void SaveListToXmlXElement<T>(IEnumerable<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Create);
                XElement xml = new XElement($"ArrayOf{typeof(T).Name}",
                                from item in list
                                select new XElement($"{typeof(T).Name}",
                                            from prop in item.GetType().GetProperties()
                                            select new XElement(prop.Name, item.GetType().GetProperty(prop.Name).GetValue(item))));
                xml.Save(file);
                file.Close();
            }
            catch (Exception)
            {
                throw new XmlFileLoadCreateException($"fail to create xml file: {filePath}");
            }
        }

        /// <summary>
        /// Load list from xml XElement
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IEnumerable<XElement> LoadListFromXmlXElement(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return from item in LoadData(filePath).Elements() select item;
                }
            }
            catch (Exception)
            {
                throw new XmlFileLoadCreateException($"fail to load xml file: {filePath}");
            }
            throw new XmlFileLoadCreateException($"{filePath} not exist at the file");
        }

        /// <summary>
        /// Load data from xml
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static XElement LoadData(string filePath)
        {
            try
            {
                return XElement.Load(filePath);
            }
            catch
            {
                throw new XmlFileLoadCreateException($"File upload problem");
            }
        }
    }
}
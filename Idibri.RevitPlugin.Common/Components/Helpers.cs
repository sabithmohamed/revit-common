using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Idibri.RevitPlugin.Common
{
    public class Helpers
    {
        public static ICommand GetCommand(string commandName, object obj)
        {
            if (obj == null) { return null; }

            PropertyInfo pi = obj.GetType().GetProperty(commandName);

            if (pi == null) { return null; }

            return pi.GetValue(obj, null) as ICommand;
        }

        public static void UpdateBindingExpressionSource(FrameworkElement element, DependencyProperty dependencyProperty)
        {
            BindingExpression bindingExpression = element.GetBindingExpression(dependencyProperty);

            if (bindingExpression != null)
            {
                bindingExpression.UpdateSource();
            }
        }

        public static T LoadFromXmlResource<T>(string path)
        {
            return LoadFromXmlResource<T>(Assembly.GetExecutingAssembly(), path);
        }

        public static T LoadFromXmlResource<T>(Assembly executingAssembly, string path)
        {
            using (StreamReader sr = new StreamReader(executingAssembly.GetManifestResourceStream(path)))
            {
                return LoadFromXmlStreamReader<T>(sr);
            }
        }

        public static T LoadFromXmlFile<T>(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                return LoadFromXmlStreamReader<T>(sr);
            }
        }

        public static T LoadFromXmlStreamReader<T>(StreamReader sr)
        {
            return (T)(new XmlSerializer(typeof(T))).Deserialize(sr);
        }

        public static void SaveXmlToFile<T>(T value, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                SaveXmlToStreamWriter(value, sw);
            }
        }

        public static void SaveXmlToStreamWriter<T>(T value, StreamWriter sw)
        {
            (new XmlSerializer(value.GetType())).Serialize(sw, value);
        }
    }
}

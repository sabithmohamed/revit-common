using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;

namespace Idibri.RevitPlugin.Common
{
    public class RevitProperty
    {
        public string Name { get; private set; }
        public Guid Id { get; private set; }
        public bool IsTypeProperty { get; private set; }
        public BuiltInParameter? BuiltInParam { get; private set; }
        public bool IgnoreMissing { get; private set; }

        static bool IsTypePropertyDefault = false;
        static bool IgnoreMissingDefault = true;

        public RevitProperty(string name, Guid id)
            : this(name, id, IsTypePropertyDefault, IgnoreMissingDefault)
        { }

        public RevitProperty(string name, Guid id, bool isTypeProperty, bool ignoreMissing)
            : this(name, isTypeProperty, ignoreMissing)
        {
            Id = id;
        }

        public RevitProperty(string name, BuiltInParameter builtInParam)
            : this(name, builtInParam, IsTypePropertyDefault)
        { }

        public RevitProperty(string name, BuiltInParameter builtInParam, bool isTypeProperty)
            : this(name, builtInParam, isTypeProperty, IgnoreMissingDefault)
        { }

        public RevitProperty(string name, BuiltInParameter builtInParam, bool isTypeProperty, bool ignoreMissing)
            : this(name, isTypeProperty, ignoreMissing)
        {
            BuiltInParam = builtInParam;
        }

        private RevitProperty(string name, bool isTypeProperty, bool ignoreMissing)
        {
            Name = name;
            IsTypeProperty = isTypeProperty;
            BuiltInParam = null;
            IgnoreMissing = ignoreMissing;
        }

        public bool HasParameter(Element element)
        {
            return GetParameter(element) != null;
        }

        public Parameter GetParameter(Element element)
        {
            return BuiltInParam.HasValue ? element.get_Parameter(BuiltInParam.Value) : element.get_Parameter(Id);
        }

        private Element GetElement(Element element)
        {
#if REVIT2016
            return IsTypeProperty ? element.Document.GetElement(element.GetTypeId()) : element;
#elif REVIT2014
            return IsTypeProperty ? element.Document.GetElement(element.GetTypeId()) : element;
#elif REVIT2012
            return IsTypeProperty ? element.Document.get_Element(element.GetTypeId()) : element;
#else
            return element;
#endif
        }

        private MissingParameterException GetMissingParameterException(Element element)
        {
            string elementName = element.get_Parameter(BuiltInParameter.ALL_MODEL_MARK).AsString();
            elementName = string.IsNullOrEmpty(elementName) ? "NO MARK" : elementName;
            return new MissingParameterException(string.Format("Element {0}: {1} is missing required parameter {2}", element.Id.IntegerValue, elementName, Name));
        }

        private void HandleMissingParameter(Element element)
        {
            if (IgnoreMissing)
            {
                return;
            }
            else
            {
                throw GetMissingParameterException(element);
            }
        }

        public bool IsPresent(Element element)
        {
            Parameter param = GetParameter(GetElement(element));
            return param != null;
        }

        public string GetString(Element element)
        {
            Parameter param = GetParameter(GetElement(element));
            if (param == null)
            {
                HandleMissingParameter(element);
                return null;
            }
            else
            {
                return param.AsString();
            }
        }

        public void SetString(Element element, string value)
        {
            Parameter param = GetParameter(GetElement(element));
            if (param == null)
            {
                HandleMissingParameter(element);
            }
            else
            {
                param.Set(value);
            }
        }

        public int GetInt(Element element)
        {
            Parameter param = GetParameter(GetElement(element));
            if (param == null)
            {
                HandleMissingParameter(element);
                return default(int);
            }
            else
            {
                return param.AsInteger();
            }
        }

        public void SetInt(Element element, int value)
        {
            Parameter param = GetParameter(GetElement(element));
            if (param == null) { throw GetMissingParameterException(element); }
            param.Set(value);
        }

        public double GetDouble(Element element)
        {
            Parameter param = GetParameter(GetElement(element));
            if (param == null)
            {
                HandleMissingParameter(element);
                return default(double);
            }
            else
            {
                return param.AsDouble();
            }
        }

        public void SetDouble(Element element, double value)
        {
            Parameter param = GetParameter(GetElement(element));
            if (param == null)
            {
                HandleMissingParameter(element);
            }
            else
            {
                param.Set(value);
            }
        }

        public string GetValueString(Element element)
        {
            Parameter param = GetParameter(GetElement(element));
            if (param == null)
            {
                HandleMissingParameter(element);
                return default(string);
            }
            else
            {
                return param.AsValueString();
            }
        }

        public void SetValueString(Element element, string value)
        {
            Parameter param = GetParameter(GetElement(element));
            if (param == null)
            {
                HandleMissingParameter(element);
            }
            else
            {
                param.SetValueString(value);
            }
        }

        public static IEnumerable<string> DebugParameters(Element element)
        {
            if (element != null)
            {
                foreach (Parameter p in element.Parameters)
                {
                    string parameterType = p.Definition != null ? p.Definition.GetDataType().TypeId : "Unknown Type";
                    yield return string.Format("{0} - {1} - {2}", p.IsShared ? p.GUID.ToString() : "Not Shared", parameterType, p.Definition.Name);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Idibri.RevitPlugin.Common.Infrastructure
{
    public class ExtendedPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        public object OldValue { get; private set; }
        public object NewValue { get; private set; }

        public ExtendedPropertyChangedEventArgs(string propertyName, object oldValue, object newValue)
            : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public class ModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private Dictionary<string, string> PropertyErrors { get; set; }

        public virtual string Error { get { throw new NotImplementedException(); } }

        public string this[string propertyName]
        {
            get
            {
                return PropertyErrors != null && PropertyErrors.ContainsKey(propertyName) ? PropertyErrors[propertyName] : null;
            }
        }

        protected void SetError(string propertyName, string errorMessage)
        {
            if (PropertyErrors == null)
            {
                PropertyErrors = new Dictionary<string, string>();
            }
            PropertyErrors[propertyName] = errorMessage;
        }

        protected void ClearError(string propertyName)
        {
            if (PropertyErrors != null && PropertyErrors.ContainsKey(propertyName))
            {
                PropertyErrors.Remove(propertyName);
            }
        }

        protected void ClearAllErrors()
        {
            if (PropertyErrors != null) { PropertyErrors.Clear(); }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void NotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        protected void NotifyPropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        protected string ReduceString(string str)
        {
            if (str == null || str.Length == 0) { return null; }
            str = str.Trim();
            return str.Length == 0 ? null : str;
        }

        protected void ChangeRegistration(INotifyPropertyChanged oldValue, INotifyPropertyChanged newValue, PropertyChangedEventHandler handler)
        {
            if (oldValue != null)
            {
                oldValue.PropertyChanged -= handler;
            }
            if (newValue != null)
            {
                newValue.PropertyChanged += handler;
            }
        }

        protected void ChangeRegistration(INotifyCollectionChanged oldValue, INotifyCollectionChanged newValue, NotifyCollectionChangedEventHandler handler)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= handler;
            }
            if (newValue != null)
            {
                newValue.CollectionChanged += handler;
            }
        }
    }
}

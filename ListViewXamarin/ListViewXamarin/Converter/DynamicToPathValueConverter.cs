using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class DynamicToPathValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;
            ExpandoObject busniessObject = JsonConvert.DeserializeObject<ExpandoObject>(value.ToString());
            var jsonList = busniessObject.ToList();
            if (parameter.Equals("Value1"))
                return jsonList[0].Value;
            if (parameter.Equals("Value2"))
                return jsonList[1].Value;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

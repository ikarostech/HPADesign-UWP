using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Reactive.Bindings;
using Windows.UI.Xaml.Media;
using Windows.Foundation;

namespace HPADesign.Services
{
    public class PointCollectionConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value.GetType() == typeof(ReactiveProperty<PointCollection>) && targetType == typeof(PointCollection))
            {
                ReactiveProperty<PointCollection> result = value as ReactiveProperty<PointCollection>;
                return result.Value;
            }
            if (value.GetType() == typeof(ReactiveCollection<Windows.Foundation.Point>) && targetType == typeof(PointCollection))
            {
                PointCollection result = new PointCollection();
                ReactiveCollection<Point> points = value as ReactiveCollection<Windows.Foundation.Point>;
                foreach (Point p in points)
                {
                    result.Add(p);
                }
                return result;
            }
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Configurations;
using PV6900.UI.Wpf.Models;

namespace PV6900.UI.Wpf.ViewModels
{
    public class TimeSpanVoltaChartVM
    {
        static TimeSpanVoltaChartVM()
        {
            var mapper = Mappers.Xy<TimeSpanVoltaPoint>()
                .X(item => item.TimeSpan)
                .Y(item => item.Volta);
            Charting.For<TimeSpanVoltaPoint>(mapper);
        }


        private DateTimeOffset _startTime = DateTimeOffset.Now;
        public ChartValues<TimeSpanVoltaPoint> Points { get; } = new();
        public void Reset()
        {
            Points.Clear();
            _startTime = DateTimeOffset.Now;
        }

        public void FetchPoint(double volta)
        {
            double timeSpan = (DateTimeOffset.Now - _startTime).TotalMilliseconds / 1000;
            TimeSpanVoltaPoint point = new()
            {
                TimeSpan = timeSpan,
                Volta = volta
            };

            if (Points.Count() >= 25)
            {
                Points.RemoveAt(0);
            }
            Points.Add(point);

        }

       
    }
}

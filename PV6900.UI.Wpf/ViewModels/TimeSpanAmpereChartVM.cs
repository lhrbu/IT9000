using LiveCharts;
using LiveCharts.Configurations;
using PV6900.UI.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV6900.UI.Wpf.ViewModels
{
    public class TimeSpanAmpereChartVM
    {
        static TimeSpanAmpereChartVM()
        {
            var mapper = Mappers.Xy<TimeSpanAmperePoint>()
               .X(item => item.TimeSpan)
               .Y(item => item.Ampere);
            Charting.For<TimeSpanAmperePoint>(mapper);
        }
        private DateTimeOffset _startTime = DateTimeOffset.Now;
        public ChartValues<TimeSpanAmperePoint> Points { get; } = new();
        public void Reset()
        {
            Points.Clear();
            _startTime = DateTimeOffset.Now;
        }

        public void FetchPoint(double ampere)
        {
            TimeSpanAmperePoint point = new()
            {
                TimeSpan = (DateTimeOffset.Now - _startTime).TotalMilliseconds / 1000,
                Ampere = ampere
            };

            if (Points.Count() >= 25)
            {
                Points.RemoveAt(0);
            }
            Points.Add(point);
        }

        
        
    }
}

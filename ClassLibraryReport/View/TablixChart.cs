using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TablixChart : ISerializable
    {
        public TablixChart() : this(default(String))
        {
        }

        public TablixChart(String type) : this(type, null)
        {
        }

        public TablixChart(String type, String subtitle) : this(type, subtitle, null)
        {
        }

        public TablixChart(String type, String subtitle, String inverted) :
            this(type, subtitle, inverted, null)
        {
        }

        public TablixChart(String type, String subtitle, String inverted, String height) :
            this(type, subtitle, inverted, height, null)
        {
        }

        public TablixChart(String type, String subtitle, String inverted, String height, String container) :
            this(type, subtitle, inverted, height, container, null)
        {
        }

        public TablixChart(String type, String subtitle, String inverted, String height,
                           String container, String fillOpacity) :
                               this(type, subtitle, inverted, height, container, fillOpacity, null)
        {
        }

        public TablixChart(String type, String subtitle, String inverted, String height,
                           String container, String fillOpacity, String legendDisabled) :
                               this(type, subtitle, inverted, height, container, fillOpacity, legendDisabled, null)
        {
        }

        public TablixChart(String type, String subtitle, String inverted, String height,
                           String container, String fillOpacity, String legendDisabled, String legendLayout) :
                               this(type, subtitle, inverted, height, container, fillOpacity, legendDisabled,
                                    legendLayout, null)
        {
        }

        public TablixChart(String type, String subtitle, String inverted, String height,
                           String container, String fillOpacity, String legendDisabled, String legendLayout,
                           String legendWidth) :
                               this(type, subtitle, inverted, height, container, fillOpacity, legendDisabled,
                                    legendLayout, legendWidth, null)
        {
        }

        public TablixChart(String type, String subtitle, String inverted, String height,
                           String container, String fillOpacity, String legendDisabled, String legendLayout,
                           String legendWidth, String legendX) :
                               this(type, subtitle, inverted, height, container, fillOpacity, legendDisabled,
                                    legendLayout, legendWidth, legendX, null)
        {
        }

        public TablixChart(String type, String subtitle, String inverted, String height,
                           String container, String fillOpacity, String legendDisabled, String legendLayout,
                           String legendWidth, String legendX, String legendY)
        {
            Type = type;
            Subtitle = subtitle;
            Inverted = inverted;
            Height = height;
            Container = container;
            FillOpacity = fillOpacity;
            LegendDisabled = legendDisabled;
            LegendLayout = legendLayout;
            LegendWidth = legendWidth;
            LegendX = legendX;
            LegendY = legendY;
        }

        public TablixChart(TablixChart chart)
        {
            Type = chart.Type;
            Subtitle = chart.Subtitle;
            Inverted = chart.Inverted;
            Height = chart.Height;
            Container = chart.Container;
            FillOpacity = chart.FillOpacity;
            LegendDisabled = chart.LegendDisabled;
            LegendLayout = chart.LegendLayout;
            LegendWidth = chart.LegendWidth;
            LegendX = chart.LegendX;
            LegendY = chart.LegendY;
        }

        public TablixChart(SerializationInfo si, StreamingContext sc)
        {
            Type = si.GetValue("Type", typeof (String)) as String;
            Subtitle = si.GetValue("Subtitle", typeof (String)) as String;
            Inverted = si.GetValue("Inverted", typeof (String)) as String;
            Height = si.GetValue("Height", typeof (String)) as String;
            Container = si.GetValue("Container", typeof (String)) as String;
            FillOpacity = si.GetValue("FillOpacity", typeof (String)) as String;
            LegendDisabled = si.GetValue("LegendDisabled", typeof (String)) as String;
            LegendLayout = si.GetValue("LegendLayout", typeof (String)) as String;
            LegendWidth = si.GetValue("LegendWidth", typeof (String)) as String;
            LegendX = si.GetValue("LegendX", typeof (String)) as String;
            LegendY = si.GetValue("LegendY", typeof (String)) as String;
        }

        public String Type { get; set; }
        public String Subtitle { get; set; }
        public String Inverted { get; set; }
        public String Height { get; set; }
        public String Container { get; set; }
        public String FillOpacity { get; set; }
        public String LegendDisabled { get; set; }
        public String LegendLayout { get; set; }
        public String LegendWidth { get; set; }
        public String LegendX { get; set; }
        public String LegendY { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Type", Type);
            si.AddValue("Subtitle", Subtitle);
            si.AddValue("Inverted", Inverted);
            si.AddValue("Height", Height);
            si.AddValue("Container", Container);
            si.AddValue("FillOpacity", FillOpacity);
            si.AddValue("LegendDisabled", LegendDisabled);
            si.AddValue("LegendLayout", LegendLayout);
            si.AddValue("LegendWidth", LegendWidth);
            si.AddValue("LegendX", LegendX);
            si.AddValue("LegendY", LegendY);
        }

        public override String ToString()
        {
            return String.Format("[ TablixChart ][ Type: {0} ][ Subtitle: {1} ]" +
                                 "[ Inverted: {2} ][ Height: {3} ][ Container: {4} ][ FillOpacity {5} ]" +
                                 "[ LegendDisabled: {6} ][ LegendLayout: {7} ][ LegendWidth: {8} ]" +
                                 "[ LegendX: {9} ][ LegendY: {10} ]",
                                 Type, Subtitle, Inverted, Height, Container, FillOpacity, LegendDisabled,
                                 LegendLayout, LegendWidth, LegendX, LegendY);
        }
    }
}
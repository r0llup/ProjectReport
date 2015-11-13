using System;
using System.Collections.Generic;
using System.Web.UI;
using ClassLibraryReport.Data;
using ClassLibraryReport.View;

namespace ClassLibraryReport.Interfaces
{
    public interface IHtmlRenderer : IRenderer
    {
        String CssDirectoryPath { get; set; }
        String JsDirectoryPath { get; set; }
        Dictionary<String, String> TranslationsDictionary { get; set; }
        Object CachedGroupValue { get; set; }
        Boolean IsGroupEnded { get; set; }
        Int32 GroupCounter { get; set; }
        Int32 RowCounter { get; set; }
        String HtmlDirectoryPath { get; set; }
        String MhtmlDirectoryPath { get; set; }
        void Render(Boolean doMhtml);
        void LoadCss(ref HtmlTextWriter writer);
        void LoadJs(ref HtmlTextWriter writer);
        void RenderJs(ref HtmlTextWriter writer, Container container);
        void RenderContainer(ref HtmlTextWriter writer, Container container);
        void RenderTablix(ref HtmlTextWriter writer, Tablix tablix);

        void RenderTablixContainer(ref HtmlTextWriter writer, TablixContainer tablixContainer,
                                   DataSet dataSet, TablixColumnHierarchy tablixColumnHierarchy);

        void RenderTablixRow(ref HtmlTextWriter writer, TablixRow tablixRow, Fields fields,
                             TablixContainer tablixContainer, TablixColumnHierarchy tablixColumnHierarchy,
                             DataSet dataSet);

        void RenderTablixCell(ref HtmlTextWriter writer, TablixCell tablixCell, Fields fields,
                              TablixContainer tablixContainer, FieldDescriptors fieldDescriptors, Stats stats);

        Object RenderStats(TablixCell tablixCell, FieldDescriptors fieldDescriptors, Stats stats);
        void RenderTextBox(ref HtmlTextWriter writer, TextBox textBox);

        void RenderTextBox(ref HtmlTextWriter writer, TextBox textBox,
                           HtmlTextWriterTag? tag);

        void RenderTextBox(ref HtmlTextWriter writer, TextBox textBox,
                           HtmlTextWriterTag? tag, Boolean useStyle);

        void RenderChart(ref HtmlTextWriter writer, Chart chart);
        String RenderStyle(String style);
        void RenderStyle(ref HtmlTextWriter writer, String style);
    }
}
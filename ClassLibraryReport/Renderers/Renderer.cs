using System;
using System.Collections.Generic;
using ClassLibraryReport.Interfaces;
using ClassLibraryReport.View;

namespace ClassLibraryReport.Renderers
{
    public abstract class Renderer : IRenderer
    {
        protected Renderer() : this(default(Reports))
        {
        }

        protected Renderer(Report report) :
            this(new Reports(new List<Report> {report}))
        {
        }

        protected Renderer(Reports reports)
        {
            Reports = reports;
        }

        protected Renderer(Renderer renderer)
        {
            Reports = new Reports(renderer.Reports);
        }

        public Reports Reports { get; set; }

        public virtual void Render()
        {
            throw new NotImplementedException();
        }

        public Boolean IsValidUri(String uri)
        {
            Uri uriResult;
            return uri != null && Uri.TryCreate(uri, UriKind.Absolute, out uriResult);
        }
    }
}
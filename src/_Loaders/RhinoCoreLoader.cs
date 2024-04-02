using System;
using System.IO;

using NUnit.Framework;

namespace Rhino.Testing
{
    static class RhinoCoreLoader
    {
        static IDisposable s_core;
        static IDisposable s_doc;

        public static void LoadCore(bool createDoc)
        {
#if NET7_0_OR_GREATER
            string[] args = new string[] { "/netcore " };
#else
            string[] args = new string[] { "/netfx " };
#endif

            s_core = new Rhino.Runtime.InProcess.RhinoCore(args);

            if (createDoc)
            {
                Rhino.RhinoDoc.ActiveDoc = Rhino.RhinoDoc.CreateHeadless(string.Empty);
                s_doc = Rhino.RhinoDoc.ActiveDoc;
            }
        }

        public static void LoadEto()
        {
            TestContext.WriteLine("Loading eto platform");

            Eto.Platform.AllowReinitialize = true;
            Eto.Platform.Initialize(Eto.Platforms.Wpf);
        }

        public static void DisposeCore()
        {
            (s_core as Rhino.Runtime.InProcess.RhinoCore)?.Dispose();
            (s_doc as Rhino.RhinoDoc)?.Dispose();

            s_core = default;
            s_doc = default;
        }
    }
}

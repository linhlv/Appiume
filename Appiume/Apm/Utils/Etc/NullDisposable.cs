﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Utils.Etc
{
    /// <summary>
    /// This class is used to simulate a Disposable that does nothing.
    /// </summary>
    internal sealed class NullDisposable : IDisposable
    {
        public static NullDisposable Instance { get { return SingletonInstance; } }
        private static readonly NullDisposable SingletonInstance = new NullDisposable();

        private NullDisposable()
        {

        }

        public void Dispose()
        {

        }
    }
}
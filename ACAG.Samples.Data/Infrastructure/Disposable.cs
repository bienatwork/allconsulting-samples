// ACAG.Samples.Data.Infrastructure
// *****************************************************************************************
//
// Name:        DbFactory.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System;

namespace ACAG.Samples.Data.Infrastructure
{
    /// <summary>
    /// Definition Disposable
    /// </summary>
    public class Disposable : IDisposable
    {
        private bool _isDisposed;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }

            _isDisposed = true;
        }

        /// <summary>
        /// Ovveride this to dispose custom objects
        /// </summary>
        protected virtual void DisposeCore()
        {
        }
    }
}

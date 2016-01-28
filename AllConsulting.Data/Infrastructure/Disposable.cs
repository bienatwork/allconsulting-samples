// Infrastructure
// *****************************************************************************************
//
// Name:		Disposable.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************
using System; 
namespace ACAG.Data.Infrastructure
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

        // Ovveride this to dispose custom objects
        protected virtual void DisposeCore()
        {
        }
    }
}

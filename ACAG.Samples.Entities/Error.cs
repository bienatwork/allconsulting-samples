// ACAG.Samples.Entities
// *****************************************************************************************
//
// Name:        Error.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System;

namespace ACAG.Samples.Entities
{
    /// <summary>
    /// Represent an error
    /// </summary>
    public class Error : IEntityBase
    {
        public int ID { get; set; }

        /// <summary>
        /// Error message of an exception
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The stacktrace of an exception
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// The time of exception thrown
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}

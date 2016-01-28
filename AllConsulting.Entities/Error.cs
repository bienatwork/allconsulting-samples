using System;

namespace ACAG.Entities
{
    /// <summary>
    /// Represents a Error
    /// </summary>
    public class Error : IEntityBase
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the StackTrace
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// Gets or sets the DateCreated
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.SharedKernel
{
    /// <summary>
    /// app config.
    /// </summary>
    public sealed class ApplicationConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether the IncludeExceptionDetailsInResponse is enabled.
        /// </summary>
        public bool IncludeExceptionDetailsInResponse { get; set; }
    }

}

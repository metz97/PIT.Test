using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models
{
    public interface ITokenContainer
    {
        object ApiToken { get; set; }

        object Username { get; set; }
    }
}
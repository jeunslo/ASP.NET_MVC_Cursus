using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Voorbeeld3
{
    public class OnevenTussenTweeGrenzenAttribute: RangeAttribute
    {
        public OnevenTussenTweeGrenzenAttribute(double minimum, double maximum) : base(minimum, maximum) { }

        public override bool IsValid(object value)
        {
            if(value == null) { return true; }
            if(!(value is int)) { return false; }
            return base.IsValid(value) && ((int)value % 2) == 1;
        }
    }
}
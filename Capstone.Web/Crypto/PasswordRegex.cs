using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Crypto
{
    public class RegExLength : RegularExpressionAttribute
    {
        public RegExLength(string pattern) : base(pattern) { }
    }

    public class RegExContainsNumber : RegularExpressionAttribute
    {
        public RegExContainsNumber(string pattern) : base(pattern) { }
    }

    public class RegExContainsLowercase : RegularExpressionAttribute
    {
        public RegExContainsLowercase(string pattern) : base(pattern) { }
    }

    public class RegExContainsUppercase : RegularExpressionAttribute
    {
        public RegExContainsUppercase(string pattern) : base(pattern) { }
    }

    public class RegExNoWhiteSpace : RegularExpressionAttribute
    {
        public RegExNoWhiteSpace(string pattern) : base(pattern) { }
    }
}
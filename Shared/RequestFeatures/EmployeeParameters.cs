using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class EmployeeParameters : RequestParameters
    {
        public EmployeeParameters()
        {
            OrderBy = "name";
            dynamic person = new ExpandoObject();
        }

        public uint MinAge { get; set; }
        public uint MaxAge { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxAge > MinAge;
    }
}

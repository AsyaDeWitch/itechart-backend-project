using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace xUnitTestProject.AutoFakeItEasyDataAttributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InlineAutoFakeItEasyDataAttribute : InlineAutoDataAttribute
    {
        private readonly object[] _values;

        public InlineAutoFakeItEasyDataAttribute(params object[] values)
        : base(new AutoFakeItEasyDataAttribute(), values)
        {
            _values = values;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var data = base.GetData(testMethod).ToList();

            for (var i = 0; i < _values.Length; i++)
            {
                data[0][i] = _values[i];
            }

            return data;
        }
    }
}

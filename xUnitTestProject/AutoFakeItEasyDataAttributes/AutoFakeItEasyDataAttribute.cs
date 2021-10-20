using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using AutoFixture.Xunit2;
using System;

namespace xUnitTestProject.AutoFakeItEasyDataAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AutoFakeItEasyDataAttribute : AutoDataAttribute
    {
        public AutoFakeItEasyDataAttribute()
        : base(FixtureFactory)
        {
        }

        private static IFixture FixtureFactory()
        {
            return new Fixture().Customize(new AutoFakeItEasyCustomization());
        }
    }
}

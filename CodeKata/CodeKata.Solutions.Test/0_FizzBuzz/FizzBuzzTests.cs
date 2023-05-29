using CodeKata.Common;
using CodeKata.Solutions._0_FizzBuzz;

namespace CodeKata.Solutions.Test
{
    public class FizzBuzzTests
    {

        public class SomethingSomethingMethod : FizzBuzzTests
        {
            [Fact]
            public void Test1()
            {
                // Arrange
                var target = CreateTarget();

                // Act
                

                // Assert

            }
        }

        private FizzBuzz CreateTarget()
        {
            return new FizzBuzz();
        }
    }
}
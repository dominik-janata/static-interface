using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StaticInterface.UnitTest
{
    [TestClass]
    public class StaticInterfaceFactoryTest
    {
        [TestMethod]
        public void CreateGeneric_AnyParam_ReturnsInstanceOfType_IfInitializationSucceeds()
        {
            // Arrange & Act
            ConstructibleFromParamArray result = StaticInterfaceFactory.Create<ConstructibleFromParamArray>(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ConstructibleFromParamArray));
            Assert.AreEqual(2, result.Number);
        }

        [TestMethod]
        public void CreateNonGeneric_AnyParam_ReturnsInstanceOfType_IfInitializationSucceeds()
        {
            // Arrange & Act
            IConstructible result = StaticInterfaceFactory.Create(typeof(ConstructibleFromParamArray), 2, 3);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ConstructibleFromParamArray));
            Assert.AreEqual(2, ((ConstructibleFromParamArray)result).Number);
        }

        [TestMethod]
        public void CreateGeneric_AnyParam_ReturnsInstanceOfType_IfInitializationSucceeds2()
        {
            // Arrange & Act
            ConstructibleFromInt result = StaticInterfaceFactory.Create<ConstructibleFromInt, int>(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ConstructibleFromInt));
            Assert.AreEqual(2, result.Number);
        }

        [TestMethod]
        public void CreateNonGeneric_AnyParam_ReturnsInstanceOfType_IfInitializationSucceeds2()
        {
            // Arrange & Act
            IConstructible<int> result = StaticInterfaceFactory.Create<int>(typeof(ConstructibleFromInt), 2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ConstructibleFromInt));
            Assert.AreEqual(2, ((ConstructibleFromInt)result).Number);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Create_AnyParam_ThrowsException_IfInitializationFails()
        {
            // Arrange, Act & Assert
            StaticInterfaceFactory.Create<ConstructibleFromParamArray>(null);
        }

        private class ConstructibleFromParamArray : IConstructible
        {
            public int Number { get; private set; }

            public void Initialize(params object[] parameters)
            {
                if (parameters[0] == null)
                {
                    throw new NullReferenceException();
                }

                this.Number = (int)parameters[0];
            }
        }

        private class ConstructibleFromInt : IConstructible<int>
        {
            public int Number { get; private set; }

            public void Initialize(int number)
            {
                if (number < 0)
                {
                    throw new ArgumentException(nameof(number));
                }

                this.Number = number;
            }
        }
    }
}

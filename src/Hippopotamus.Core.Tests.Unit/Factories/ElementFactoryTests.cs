using Hippopotamus.Core.Factories;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Hippopotamus.Core.Tests.Unit.Factories
{
    public class ElementFactoryTests
    {
        [Test]
        public void Create_AllParamsAreValid_CreatesElement()
        {
            // Arrange
            var elementToCreateType = typeof(DummyElement);
            var parentMock = new Mock<IBlock>();
            var specification = By.Id("");

            // Act
            var elementAsObject = ElementFactory.Create(elementToCreateType, parentMock.Object, specification);

            // Assert
            Assert.IsNotNull(elementAsObject);
            var element = elementAsObject as DummyElement;
            Assert.IsNotNull(element);
            Assert.AreSame(parentMock.Object, element.Parent);
            Assert.AreEqual(AccessibilityLevel.Visible, element.AccessibilityLevel);
            Assert.AreEqual(true, element.AfterCreatedInvoked);
        }

        [Test]
        public void Create_ElementToCreateTypeIsNull_ThrowsException()
        {
            // Arrange
            var parentMock = new Mock<IBlock>();
            var specification = By.Id("");

            void Create()
            {
                ElementFactory.Create(null, parentMock.Object, specification);
            }

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(Create);

            // Assert
            Assert.AreEqual("elementToCreateType", exception.ParamName);
        }

        [Test]
        public void Create_ElementToCreateTypeIsNotValid_ThrowsException()
        {
            // Arrange
            var elementToCreateType = typeof(object);
            var parentMock = new Mock<IBlock>();
            var specification = By.Id("");

            void Create()
            {
                ElementFactory.Create(elementToCreateType, parentMock.Object, specification);
            }

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(Create);

            // Assert
            Assert.AreEqual("elementToCreateType", exception.ParamName);
        }

        [Test]
        public void Create_ElementToCreateTypeDoNotHaveValidContructor_ThrowsException()
        {

            // Arrange
            var elementToCreateType = typeof(BlockWithoutExpectedContructor);
            var parentMock = new Mock<IBlock>();
            var specification = By.Id("");

            void Create()
            {
                ElementFactory.Create(elementToCreateType, parentMock.Object, specification);
            }

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(Create);

            // Assert
            Assert.AreEqual("elementToCreateType", exception.ParamName);
        }

        [Test]
        public void Create_ParentIsNull_ThrowsException()
        {
            // Arrange
            var elementToCreateType = typeof(DummyElement);
            var specification = By.Id("");

            void Create()
            {
                ElementFactory.Create(elementToCreateType, null, specification);
            }

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(Create);

            // Assert
            Assert.AreEqual("parent", exception.ParamName);
        }

        [Test]
        public void Create_SpecificationTypeIsNull_ThrowsException()
        {
            // Arrange
            var elementToCreateType = typeof(DummyElement);
            var parentMock = new Mock<IBlock>();

            void Create()
            {
                ElementFactory.Create(elementToCreateType, parentMock.Object, null);
            }

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(Create);

            // Assert
            Assert.AreEqual("specification", exception.ParamName);
        }

        [Test]
        public void CreateGeneric_AllParamsAreValid_CreatesElement()
        {
            // Arrange
            var parentMock = new Mock<IBlock>();
            var specification = By.Id("");

            // Act
            var element = ElementFactory.Create<DummyElement>(parentMock.Object, specification);

            // Assert
            Assert.IsNotNull(element);
            Assert.AreSame(parentMock.Object, element.Parent);
            Assert.AreEqual(AccessibilityLevel.Visible, element.AccessibilityLevel);
            Assert.AreEqual(true, element.AfterCreatedInvoked);
        }

        [Test]
        public void CreateGeneric_ParentIsNull_ThrowsException()
        {
            // Arrange
            var specification = By.Id("");

            void Create()
            {
                ElementFactory.Create<DummyElement>(null, specification);
            }

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(Create);

            // Assert
            Assert.AreEqual("parent", exception.ParamName);
        }

        [Test]
        public void CreateGeneric_ByIsNull_ThrowsException()
        {
            // Arrange
            var parentMock = new Mock<IBlock>();

            void Create()
            {
                ElementFactory.Create<DummyElement>(parentMock.Object, null);
            }

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(Create);

            // Assert
            Assert.AreEqual("specification", exception.ParamName);
        }

        private class DummyElement : Element
        {
            public DummyElement(IBlock parent, By by)
                : base(parent, by)
            {
            }

            public bool AfterCreatedInvoked { get; private set; }

            public override void AfterCreated()
            {
                AfterCreatedInvoked = true;
            }
        }

        private class BlockWithoutExpectedContructor : Block
        {
            public BlockWithoutExpectedContructor()
                : base(null, null)
            {
            }
        }
    }
}

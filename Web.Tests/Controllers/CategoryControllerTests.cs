using System;
using DataAccess.Model;
using DataAccess.Repositories;
using Moq;
using NUnit.Framework;
using Web.Controllers;

namespace Web.Tests.Controllers
{
    [TestFixture]
    public class CategoryControllerTests
    {
        private CategoryController _controller;

        [SetUp]
        public void SetUp()
        {
            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository
                .Setup(rep => rep.GetAll())
                .Returns(new[]
                {
                    new Category
                    {
                        Name = "f",
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    new Category
                    {
                        Name = "s",
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    }
                });

            _controller = new CategoryController(categoryRepository.Object);
        }

        [Test]
        public void GetSelect_should_return_select_control()
        {
            var result = _controller.GetSelect();

            Assert.That(result, Is.EqualTo("\r\n<select> \r\n    <option value=''></option><option value='00000000-0000-0000-0000-000000000001'>f</option> <option value='00000000-0000-0000-0000-000000000001'>s</option> </select>"));
        }
    }
}
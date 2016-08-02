using System;
using System.Web.Http.Results;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopListAPI.Controllers.Api;
using ShopListAPI.Core;
using ShopListAPI.Core.Dtos;
using ShopListAPI.Core.Repositories;

namespace ShopListAPI.Tests.Controllers.Api
{
    [TestClass]
    public class ShopListTest
    {
        private ShoppingListController _controller;

        public ShopListTest()
        {
            var mocRepository = new Mock<IShopItemRepository>();
            var mocUnitOfWork = new Mock<IUnitOfWork>();
            _controller = new ShoppingListController(mocUnitOfWork.Object);
            mocUnitOfWork.SetupGet(u => u.ShopItems).Returns(mocRepository.Object);
        }

        [TestMethod]
        public void GetShopItemByName_NameDoNotExistInDatabase_NotFound()
        {
            var result = _controller.GetShopItemById("MarshMallow");
            result.Should().BeOfType<NotFoundResult>();
        }
        [TestMethod]
        public void GetShopItemByName_NameExistInDatabase_FoundItem()
        {
            var result = _controller.GetShopItemById("Pepsi");
            result.Should().BeOfType<OkResult>();
        }
        // i might have forgotten to mo something... unit test is failing...
        // but using fiddler... the controller works fine....
     
    }
}

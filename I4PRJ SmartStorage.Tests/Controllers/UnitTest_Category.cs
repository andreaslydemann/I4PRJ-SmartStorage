﻿using System;
using System.Web;
using System.Web.Mvc;
using NSubstitute;
using SmartStorage.BLL.Interfaces.Services;
using NUnit.Framework;
using SmartStorage.BLL.Dtos;
using SmartStorage.BLL.Services;
using SmartStorage.BLL.ViewModels;
using SmartStorage.DAL.Context;
using SmartStorage.DAL.Interfaces;
using SmartStorage.DAL.Interfaces.Repositories;
using SmartStorage.DAL.Models;
using SmartStorage.DAL.Repositories;
using SmartStorage.DAL.UnitOfWork;
using SmartStorage.UI.Controllers;

namespace I4PRJ_SmartStorage.UnitTests.Controllers
{
    [TestFixture]
    class UnitTest_Category
    {
        private IRepository<Category> _repository;
        private IUnitOfWork _unitOfWork;
        private CategoriesController _controller;
        private ICategoryService _service;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _repository = Substitute.For<IRepository<Category>>();
            _controller = new CategoriesController(_service);
        }

        [Test]
        public void Category_LoadCategoryIndex_ReturnsCategoryIndexView()
        {
            var result = _controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void Category_CategoryCreate_ReturnsCategoryIndexView()
        {
            var viewModel = new CategoryEditModel
            {
                Category = new CategoryDto() { ByUser = "Admin",CategoryId = 1,IsDeleted = false, Name = "Test", Updated = DateTime.Now}
            };
            var result = _controller.Create(viewModel) as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}

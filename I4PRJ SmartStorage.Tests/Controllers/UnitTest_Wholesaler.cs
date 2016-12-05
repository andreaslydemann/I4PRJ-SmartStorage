﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using I4PRJ_SmartStorage.Controllers;
using NUnit.Framework;

namespace I4PRJ_SmartStorage.UnitTests.Controllers
{
    [TestFixture]
    class UnitTest_Wholesaler
    {
        private readonly WholesalersController _who = new WholesalersController();

        [Test]
        public void WholesalerIndex_LoadWholesalerIndex_ReturnsWholesalerIndexView()
        {
            var result = _who.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);

        }
    }
}

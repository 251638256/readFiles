// <copyright file="ProgramTest.cs">Copyright ©  2017</copyright>
using System;
using System.Collections.Generic;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using readFiles;

namespace readFiles.Tests
{
    /// <summary>This class contains parameterized unit tests for Program</summary>
    [PexClass(typeof(Program))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class ProgramTest
    {
        /// <summary>Test stub for FUCK()</summary>
        [PexMethod]
        internal IEnumerable<int> FUCKTest()
        {
            
            throw new Exception("异常了异常了异常了异常了异常了");
            return result;
            // TODO: add assertions to method ProgramTest.FUCKTest()
        }
    }
}

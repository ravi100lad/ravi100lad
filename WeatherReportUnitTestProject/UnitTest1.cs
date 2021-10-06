using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherReport.Common;
using WeatherReport.Interface;
using WeatherReport.ViewModel;

namespace WeatherReportUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private readonly ICommon _common;
        [TestMethod]
        public void UnitTestForLocationSearchSuccessfully()
        {
            //Arrange
            
            var loctnviewmodel = new LocationViewModel(_common);
            
           
            Mock<CommonMethods> chk = new Mock<CommonMethods>();
           
            
            chk.Setup(x => x.GetWeatherData("1235"));
            
            //Act
            loctnviewmodel.btnRefreshCommand.Execute(null);

            //Assert
            Assert.IsTrue(loctnviewmodel.LocationList.Count > 0);

        }
    }
}

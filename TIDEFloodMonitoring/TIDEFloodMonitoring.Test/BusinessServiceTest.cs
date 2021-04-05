using FakeItEasy;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TIDEFloodMonitoring.Models.Configuration;
using TIDEFloodMonitoring.Service;
using TIDEFloodMonitoring.Service.Interface;

namespace TIDEFloodMonitoring.Test
{
    public class BusinessServiceTest
    {
        private IApiService _apiService;
        private IBusinessService _businessService;
        private IOptionsSnapshot<APIConfiguration> _apiConfig;
        private readonly string _testFloodResponseString = "{   \"@context\" : \"http://environment.data.gov.uk/flood-monitoring/meta/context.jsonld\" ,  \"meta\" : {     \"publisher\" : \"Environment Agency\" ,    \"licence\" : \"http://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/\" ,    \"documentation\" : \"http://environment.data.gov.uk/flood-monitoring/doc/reference\" ,    \"version\" : \"0.9\" ,    \"comment\" : \"Status: Beta service\" ,    \"hasFormat\" : [ \"http://environment.data.gov.uk/flood-monitoring/id/floods.csv?county=Buckinghamshire\", \"http://environment.data.gov.uk/flood-monitoring/id/floods.rdf?county=Buckinghamshire\", \"http://environment.data.gov.uk/flood-monitoring/id/floods.ttl?county=Buckinghamshire\", \"http://environment.data.gov.uk/flood-monitoring/id/floods.html?county=Buckinghamshire\" ]  }   ,  \"items\" : [ {     \"@id\" : \"http://environment.data.gov.uk/flood-monitoring/id/floods/062FAGCheshamGW\" ,    \"description\" : \"Groundwater flooding in Chesham\" ,    \"eaAreaName\" : \"Hertfordshire and North London\" ,    \"eaRegionName\" : \"No longer used\" ,    \"floodArea\" : {       \"@id\" : \"http://environment.data.gov.uk/flood-monitoring/id/floodAreas/062FAGCheshamGW\" ,      \"county\" : \"Buckinghamshire\" ,      \"notation\" : \"062FAGCheshamGW\" ,      \"polygon\" : \"http://environment.data.gov.uk/flood-monitoring/id/floodAreas/062FAGCheshamGW/polygon\" ,      \"riverOrSea\" : \"Groundwater\"    }     ,    \"floodAreaID\" : \"062FAGCheshamGW\" ,    \"isTidal\" : false ,    \"message\" : \"Groundwater levels have responded to high autumn and winter rainfall and have risen to high levels at our observation borehole at Ashley Green, Chesham.\\n\\nGroundwater levels are unusually high for this time of year and have now reached a level where communities in Chesham such as the Vale Road area are at risk of being affected by flooding from groundwater.\\n\\nWe expect levels to remain high over the coming weeks in response to the high winter rainfall. Low lying land and roads will therefore be at risk of flooding.\\n\\nDue to the nature of groundwater behaviour this situation could continue for several weeks or longer. We are continuing to monitor groundwater levels and will update this message on Wednesday 7th April or as the situation changes.\" ,    \"severity\" : \"Flood alert\" ,    \"severityLevel\" : 3 ,    \"timeMessageChanged\" : \"2021-03-31T15:21:00\" ,    \"timeRaised\" : \"2021-03-31T15:21:38\" ,    \"timeSeverityChanged\" : \"2021-02-11T17:16:00\"  }   ]}";

        [SetUp]
        public void Setup()
        {
            _apiService = A.Fake<IApiService>();
            _apiConfig = A.Fake<IOptionsSnapshot<APIConfiguration>>();
            _businessService = new BusinessService(_apiService, _apiConfig);
        }

        [Test]
        public async Task GetFloodWarning_WhenGetBlows_ReturnException()
        {
            A.CallTo(() => _apiConfig.Value).Returns(new APIConfiguration());
            A.CallTo(() => _apiService.Get(A<string>._, A<string>._, A<string>._)).Throws(new Exception("No Base Url"));

            var result = await _businessService.GetFloodWarning("");

            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Message.Contains("Exception"));
        }

        [Test]
        public async Task GetFloodWarning_WhenGetIsNotSuccess_ReturnMessage()
        {
            A.CallTo(() => _apiConfig.Value).Returns(new APIConfiguration() { FloodBaseUrl = "https://look-at-me-i-am-a-url-wow/" });
            A.CallTo(() => _apiService.Get(A<string>._, A<string>._, A<string>._)).Returns(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));

            var result = await _businessService.GetFloodWarning("");

            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Message.Contains("Unsuccessfull"));
        }

        [Test]
        public async Task GetFloodWarning_WhenGetIsSuccess_ReturnFloodResponse()
        {
            A.CallTo(() => _apiConfig.Value).Returns(new APIConfiguration() { FloodBaseUrl = "https://look-at-me-i-am-a-url-wow/" });
            A.CallTo(() => _apiService.Get(A<string>._, A<string>._, A<string>._)).Returns(new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(_testFloodResponseString) });

            var result = await _businessService.GetFloodWarning("A Real County!");

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Items.Any());
        }
    }
}

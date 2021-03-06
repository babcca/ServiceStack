using System.Runtime.Serialization;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.ServiceModel;

namespace ServiceStack.WebHost.Endpoints.Tests
{
	[DataContract]
	public class WithStatusResponse
	{
		[DataMember]
		public ResponseStatus ResponseStatus { get; set; }
	}

	[DataContract]
	public class NoStatusResponse
	{
	}

	[TestFixture]
	public class WebServiceExceptionTests
	{
		[Test]
		public void Can_retrieve_Errors_from_Dto_WithStatusResponse()
		{
			var webEx = new WebServiceException
			{
				ResponseDto = new WithStatusResponse
				{
					ResponseStatus = new ResponseStatus
					{
						ErrorCode = "errorCode",
						Message = "errorMessage",
						StackTrace = "stackTrace"
					}
				}
			};

			Assert.That(webEx.ErrorCode, Is.EqualTo("errorCode"));
			Assert.That(webEx.ErrorMessage, Is.EqualTo("errorMessage"));
			Assert.That(webEx.ServerStackTrace, Is.EqualTo("stackTrace"));
		}

		[Test]
		public void Can_retrieve_empty_Errors_from_Dto_NoStatusResponse()
		{
			var webEx = new WebServiceException
			{
				ResponseDto = new NoStatusResponse()
			};

			Assert.That(webEx.ErrorCode, Is.Null);
			Assert.That(webEx.ErrorMessage, Is.Null);
			Assert.That(webEx.ServerStackTrace, Is.Null);
		}

	}

}
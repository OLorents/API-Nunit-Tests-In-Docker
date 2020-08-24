using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace WebApp.Tests
{
    public class ProductsTests
    {
        public RestClient Client;
        public string BaseUrl = "http://localhost:61536/api/product";

        public string DbConnectionProduct =
            @"Data Source = {DB}; uid={uid}; pwd={pwd}; Initial Catalog = ProductsDb;";

        [SetUp]
        public void Setup()
        {
            Client = new RestClient(BaseUrl);
        }

        [Test]
        public void GetProducts()
        {
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = Client.Execute(request);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Status Code is not 'OK'");
                Assert.AreEqual(200, (int) response.StatusCode, "Status Code is not '200'");
                Assert.IsTrue(
                    response.Content.Contains(
                        "{\"productId\":1,\"productName\":\"Iphone\",\"productPrice\":\"1000\"}"));
            });
        }

        [Test]
        public void GetProductById()
        {
            RestRequest request = new RestRequest("2", Method.GET);
            var response = Client.Execute(request);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Status Code is not 'OK'");
                Assert.AreEqual(200, (int) response.StatusCode, "Status Code is not '200'");
            });
        }

        [Test]
        public void GetProductByWrongId()
        {
            RestRequest request = new RestRequest("3", Method.GET);
            var response = Client.Execute(request);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode,
                    "Status Code is not 'NotFound'");
                Assert.AreEqual(404, (int) response.StatusCode, "Status Code is not '404'");
                Assert.AreEqual("No Record Found...", ParseUpdateResponse(response),
                    "'No Record Found...' message should be displayed");
            });
        }

        [Test]
        public void CreateProduct()
        {
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new {productName = "Test", productPrice = "1000"});
            var response = Client.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Status Code is not 'Created'");
        }

        [Test]
        public void UpdateProduct()
        {
            var request = new RestRequest("7", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new {ProductId = "7", productName = "Phone", productPrice = "1000"});
            var response = Client.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Status Code is not 'Created'");
            Assert.AreEqual("Product Updated...", ParseUpdateResponse(response), "Status Code is not 'Created'");
        }

        protected List<ResponseBody> ParseResponse(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<List<ResponseBody>>(response.Content);
        }

        protected string ParseUpdateResponse(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<string>(response.Content);
        }

        private string getProductName(int id)
        {
            var sqlScript =
                "Select ProductName from ProductsDb.dbo.Products" +
                $" Where ProductId = {id}";
            using (var conn = new SqlConnection(DbConnectionProduct))
            {
                using (var command = new SqlCommand(sqlScript, conn))
                {
                    var sqlDa = new SqlDataAdapter(command);
                    var ds = new DataSet();

                    sqlDa.Fill(ds);

                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
        }
    }
}
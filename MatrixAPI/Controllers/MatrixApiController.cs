using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MatrixAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("text/json")]
    public class MatrixApiController : ControllerBase
    {

        private readonly ILogger<MatrixApiController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MatrixApiController(ILogger<MatrixApiController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Endpoint que realiza las multiplicaciones de matrices
        /// </summary>
        /// <returns>Devuelve las matrices multiplicadas o el error con su respectiva explicación</returns>
        [HttpGet("MatrixProduct")]
        public IEnumerable<MatrixResponse> Get()
        {
            var response = new MatrixResponse
            {
                Result = "Response OK"
            };
            yield return response;
        }

        [HttpPost]
        public dynamic Post([FromBody] dynamic response)
        {
            var json = JObject.Parse(response.ToString());

            JToken jToken = json.GetValue("matrixA");
            var matrixA = jToken.Select(x => x.Values<int>().ToArray()).ToArray();

            jToken = json.GetValue("matrixB");
            var matrixB = jToken.Select(x => x.Values<int>().ToArray()).ToArray();

            return new CreatedAtRouteResult("Resultado", new { result = product(matrixA, matrixB) });
        }

        static double[][] product(int[][] matrixA, int[][] matrixB)
        {
            int RowA = matrixA.Length;
            int RowB = matrixB.Length;
            int ColA = matrixA[0].Length;
            int ColB = matrixB[0].Length;

            if (RowB != ColA)
                throw new Exception("Unable to perform Matrix Multiplication");

            double[][] result = Initializa(RowA, ColB);

            Parallel.For(0, RowA, i =>
              {
                  for (int j = 0; j < ColB; ++j) 
                      for (int k = 0; k < ColA; ++k) 
                          result[i][j] += matrixA[i][k] * matrixB[k][j];
              }
            );
            return result;
        }

        static double[][] Initializa(int row, int col)
        {
            double[][] result = new double[row][];
            for (int i = 0; i < row; ++i)
                result[i] = new double[col];
            return result;
        }

    }
}

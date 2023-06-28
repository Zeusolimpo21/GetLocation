using GetLocation.Class;
using GetLocation.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using static GetLocation.Class.Trilateration;

namespace GetLocation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PosicionController : ControllerBase
    {
        [HttpPost]
        [Produces("application/json")]
        [Route("topsecret")]
        public dynamic topsecret([FromBody] List<Satelites> datos)
        {
            string error = string.Empty;
            try
            {
                double d1 = datos[0].distancia;
                double d2 = datos[1].distancia;
                double d3 = datos[2].distancia;

                string[] s1 = datos[0].mensaje;
                string[] s2 = datos[1].mensaje;
                string[] s3 = datos[2].mensaje;

                var result = GetLocation(d1, d2, d3);
                string mensajeFinal = GetMessage(s1, s2, s3);

                result.mensaje = mensajeFinal;
                return result;
            }
            catch (Exception ex)
            {
               error=ex.ToString();
            }
            return error;
        }

        private Coordenadas GetLocation(double r1, double r2, double r3)
        {
            point coordenadas = Trilateration.Compute(r1, r2, r3);
            return new Coordenadas
            {
                valor1 = coordenadas.x,
                valor2 = coordenadas.y
            };
        }

        private string GetMessage(string[] m1, string[] m2, string[] m3)
        {
            string mensaje = string.Empty;

            for (int i = 0; i < m1.Length; i++)
            {
                
                if (m1[i].ToString() != string.Empty) 
                    { mensaje = mensaje + m1[i].ToString() + " "; }
                else if (m2[i].ToString() != string.Empty)
                    { mensaje = mensaje + m2[i].ToString() + " "; }
                else
                    { mensaje = mensaje + m3[i].ToString() + " "; }
            }
            return mensaje;
        }
    }
}

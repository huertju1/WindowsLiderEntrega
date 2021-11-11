using System.Collections.Generic;

namespace WindowsLiderEntrega.Servicios
{
    public class ConsumoServicio
    {
        ServicioPrueba.ServiceClient client;

        /// <summary>
        /// Método para obtener la información de las transacciones
        /// </summary>
        /// <returns>Arreglo de transacciones</returns>
        public ServicioPrueba.Transaccion[] ConsultaDatos()
        {

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            client = new ServicioPrueba.ServiceClient();

            var resp = client.GetData("usuariop", "passwordp");
            return resp;
        }

        /// <summary>
        /// Método mediante el cual realizamos la obtención de la clave para realizar el decifrado 
        /// para el tipo de movimiento
        /// </summary>
        /// <param name="cuentaActual">Cuenta que se esta procesando</param>
        /// <returns>String con la clave actual</returns>
        public string ObtenerClaveCifrado(long cuentaActual)
        {
            client = new ServicioPrueba.ServiceClient();
            return client.GetClaveCifradoCuenta("usuariop", "passwordp", cuentaActual);
        }
        
        /// <summary>
        /// Método para realizar el envió de los saldos finales
        /// </summary>
        /// <param name="saldos">Listado de saldos obtenidos</param>
        public void GuardarInformacion(List<ServicioPrueba.Saldo> saldos)
        {
            client = new ServicioPrueba.ServiceClient();
            client.SaveData("usuariop", "passwordp", saldos.ToArray());
        }
    }
}

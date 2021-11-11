using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsLiderEntrega.Servicios;

namespace WindowsLiderEntrega.Negocio
{
    public class CalcularSaldos
    {
        /// <summary>
        /// Método para realizar el calculo de los saldos
        /// </summary>
        /// <param name="progressBar1"></param>
        /// <returns>Listado de saldos finales</returns>
        public List<ServicioPrueba.Saldo> Calcular(System.Windows.Forms.ProgressBar progressBar1)
        {
            var consumo = new ConsumoServicio();
            var transaccions = consumo.ConsultaDatos();

            int iTransaccion = 0;

            var consolidatedChildren = (from c in transaccions
                                        group c by new
                                        {
                                            c.CuentaOrigen,
                                            c.TipoTransaccion,
                                        } into gcs
                                        select new

                                        {
                                            Cuenta = gcs.Key.CuentaOrigen,
                                            tipoTransaccion = gcs.Key.TipoTransaccion,
                                            Children = gcs.ToList(),
                                        }).ToList();

            progressBar1.Maximum = consolidatedChildren.Count();
            List<ServicioPrueba.Saldo> saldos = new List<ServicioPrueba.Saldo>();

            foreach (var item in consolidatedChildren)
            {
                iTransaccion++;
                var cuentaActual = item.Cuenta;
                var claveCifrado = consumo.ObtenerClaveCifrado(cuentaActual);
                var movimientoActual = new DescifrarBO().Desencripta(claveCifrado, item.tipoTransaccion);

                foreach (var transaccion in item.Children)
                {
                 
                    //Obtenemos el saldo actual de la cuenta

                    if (saldos.Where(e => e.CuentaOrigen == cuentaActual).Count() == 0)
                    {
                        ServicioPrueba.Saldo saldo = new ServicioPrueba.Saldo();
                        saldo.CuentaOrigen = cuentaActual;
                        saldos.Add(saldo);
                    }


                    if (movimientoActual == "Debito")
                    {
                        saldos.Where(e => e.CuentaOrigen == cuentaActual).FirstOrDefault().SaldoCuenta -= transaccion.ValorTransaccion;
                    }
                    else
                    {
                        double comision = CalcularComision(Convert.ToInt64(transaccion.ValorTransaccion));
                        saldos.Where(e => e.CuentaOrigen == cuentaActual).FirstOrDefault().SaldoCuenta += transaccion.ValorTransaccion - comision;
                    }


                }

                progressBar1.Value = iTransaccion;
            }
            return saldos;
        }

        /// <summary>
        /// Método para calcular la comisión de transacciones
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public long CalcularComision(long n)
        {

            int count = 0;
            long a = 0;
            while (count < n)
            {
                a = 2;

                long b = 2;
                int prime = 1;
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                {
                    count++;
                }
                a++;
            }
            return (--a);
        }
    }
}

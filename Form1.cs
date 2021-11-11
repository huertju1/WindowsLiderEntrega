//PRUEBA OLIMPIA
//La función de la aplicación actual es calcular el saldo final de las cuentas de un "banco", para esto se consume un servicio que devuelve 
//las transacciones realizas a la cuentas.

//Paso 1: Hacer funcionar la aplicación. Debido al aumento de transacciones y al  colocar al servicio con SSL la aplicación actual esta fallando.
//Paso 2: Estructurar mejor el codigo. Uso de patrones, buenas practicas, etc.
//Paso 3: Optimizar el codigo, como se menciono en el paso 1 el aumento de transacciones ha causado que el calculo de los saldos se demore demasiado.
//Paso 4: Adicionar una barra de progreso al formulario. Actualizar la barra con el progreso del proceso, evitando bloqueos del GUI.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsLiderEntrega.Negocio;
using WindowsLiderEntrega.Servicios;

namespace WindowsLiderEntrega
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            //Variable donse se almacenan los saldos finales
            List<ServicioPrueba.Saldo> saldos = new List<ServicioPrueba.Saldo>();
            saldos = new CalcularSaldos().Calcular(progressBar1);
            sw.Stop();
            lblTiempoTotal.Text = sw.ElapsedMilliseconds.ToString();
            //Enviamos los saldos finales
            //client.SaveData("usuariop", "passwordp", saldos.ToArray());
            new ConsumoServicio().GuardarInformacion(saldos);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

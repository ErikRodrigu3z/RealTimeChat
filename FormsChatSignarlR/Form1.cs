using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsChatSignarlR
{
    public partial class Form1 : Form
    {
        private string url = "https://localhost:44388/chatHub";
        
        //conexion
        HubConnection connection;


        public Form1()
        {
            InitializeComponent();

            //se hace la conexion con la url
            connection = new HubConnectionBuilder().WithUrl(url).Build();

            //en caso de desconexion se intentara conectar de nuevo 
            connection.Closed += async (error) =>
            {
                Thread.Sleep(5000);  // cada 5 segundos
                await connection.StartAsync();
            };
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                await connection.StartAsync();

                //MessageReceiver
                connection.On<string, string>("MessageReceiver", (usuario, mensaje) =>
                {

                    txtChat.Text = txtChat.Text + "\r\n" + usuario.ToUpper() + ": " + mensaje + "\r\n";


                });

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text;
                string mensaje = txtMensaje.Text;
                await connection.InvokeAsync("MessageSender", usuario, mensaje);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Newtonsoft.Json;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace ViaCepUWP
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            string postalcode = cep.Text.Replace("-", "");
            string mode = "json";
            // Parametros do Request. Parametros de acordo com documentação em: https://viacep.com.br/
            var url = String.Format("https://viacep.com.br/ws/{0}/{1}/", postalcode, mode);
            // Chamar assincronamente o método REST API.  
            var response = await client.GetAsync(url);
            // Pegar assincronamente a resposta JSON.
            string contentString = await response.Content.ReadAsStringAsync();
            coordinatesTxtBox.Text = contentString;

            // Deserializar a resposta JSON.
            Cep resultadoCep = JsonConvert.DeserializeObject<Cep>(contentString);
            // Atualizar os campos da tela com os dados do objeto.
            numeroCEP.Text = resultadoCep.cep;
            nomeLogradouro.Text = resultadoCep.logradouro;
            
            client.Dispose();
        }

        public class Cep
        {
            public string cep { get; set; }
            public string logradouro { get; set; }
            public string complemento { get; set; }
            public string bairro { get; set; }
            public string localidade { get; set; }
            public string uf { get; set; }
            public string ibge { get; set; }
            public string gia { get; set; }
            public string ddd { get; set; }
            public string siafi { get; set; }

        }
    }
}

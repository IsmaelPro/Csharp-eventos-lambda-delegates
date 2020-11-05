using ByteBank.Agencias.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ByteBank.Agencias
{
    /// <summary>
    /// Interaction logic for EdicaoAgencia.xaml
    /// </summary>
    /// 


    //Delegates, são apontadores para funções, ou seja, 
    //são funções que nos permitem invocar outras. Eventos, 
    //utilizam os delegates para fazer a ponte entre as mensagens da aplicação e as nossas funções.


    public partial class EdicaoAgencia : Window
    {
        private readonly Agencia _agencia;
        public EdicaoAgencia(Agencia agencia)
        {
            InitializeComponent();

            _agencia = agencia ?? throw new ArgumentNullException(nameof(agencia));
            AtualizarCamposDeTexto();
            AtualizarControles();

        }

        private void AtualizarCamposDeTexto()
        {
            txtNumero.Text = _agencia.Numero;
            txtNome.Text = _agencia.Nome;
            txtTelefone.Text = _agencia.Telefone;
            txtEndereco.Text = _agencia.Endereco;
            txtDescricao.Text = _agencia.Descricao;
        }

        private void AtualizarControles()
        {
            RoutedEventHandler dialogResultTrue = (o, e) => DialogResult = true; // criando métodos anônimos para criar delegates

            RoutedEventHandler dialogResultFalse = (o, e) => DialogResult = false; //usando expressões lambda em um método anônimo


            var okEventHandler = dialogResultTrue + Fechar; // está acontecendo a mesma coisa que no método abaixo
            var cancelarEventHandler =
                (RoutedEventHandler)Delegate.Combine(  //uma forma de combinar dois métodos usando método combine
                    dialogResultFalse,
                    (RoutedEventHandler)Fechar);


            btnOK.Click += okEventHandler;
            btnCancelar.Click += cancelarEventHandler;

            txtNumero.Validacao += ValidarCampoNulo;
            txtNumero.Validacao += ValidarSomenteDigito;

            txtNome.Validacao += ValidarCampoNulo;
            txtDescricao.Validacao += ValidarCampoNulo;
            txtEndereco.Validacao += ValidarCampoNulo;
            txtTelefone.Validacao += ValidarCampoNulo;


        }

        private bool ValidarSomenteDigito(string texto)
        {
            return texto.All(char.IsDigit);
        }


        private bool ValidarCampoNulo(string texto)
        {
            return  !String.IsNullOrEmpty(texto);
        }

        private void Fechar(object sender, EventArgs e) =>
            Close();


    }
}

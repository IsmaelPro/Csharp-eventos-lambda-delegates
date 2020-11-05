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

            txtNome.TextChanged += ValidarCampoNulo;
            txtDescricao.TextChanged += ValidarCampoNulo;
            txtEndereco.TextChanged += ValidarCampoNulo;
            txtNumero.TextChanged += ValidarCampoNulo;
            txtTelefone.TextChanged += ValidarCampoNulo;


        }
        /*
         * O sender, ou o 'o' se não estivermos seguindo a regra do dotnet
         * é o objeto que que gera o evento
         * ao invés de criar um método que gera delegates podemos usar
         * repetidamente o evento em si
         * nesse caso usando o sender e dando um cast, pois ele é um objeto
         * e precisamos de um delegate.
         * Assim transformamos o objeto do evento em um delegate e o reutilizamos quantas vezes quisermos
         * 
        */

        private void ValidarCampoNulo(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            var textoEstaVazio = String.IsNullOrEmpty(txt.Text);
            txt.Background = textoEstaVazio
               ? new SolidColorBrush(Colors.OrangeRed)
               : new SolidColorBrush(Colors.White);
        }

        //private TextChangedEventHandler ConstruirDelegateValidacaoCampoNulo(TextBox txt) //criando um método que cria delegates
        //{
        //    return (o, e) =>
        //    /*um detalhe que não escrevi é que a partír do C# 3  nesse caso de delegates não precisamos
        //     * escrever os parâmetros completos com os tipos, pois o próprio c# infere o que eles são
        //     * não escrever object o, objecteventhandler e => apenas 'o' e 'e', facilitando a escrita
        //    */

        //    {
        //        var textoEstaVazio = String.IsNullOrEmpty(txt.Text);
        //         txt.Background = textoEstaVazio 
        //        ? new SolidColorBrush(Colors.OrangeRed) 
        //        : new SolidColorBrush(Colors.White);
        //    };
        //}




        private void Fechar(object sender, EventArgs e) =>
            Close();


    }
}
